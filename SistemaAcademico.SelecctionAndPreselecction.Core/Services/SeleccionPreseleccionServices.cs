using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaAcademico.Persistence.Data;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Services;

public class PreseleccionService : IPreseleccionService
{
    private readonly IPreseleccionRepository _preseleccionRepository;
    private readonly ISeleccionRepository _seleccionRepository;
    private readonly IPeriodoConfigService _periodoConfigService;
    private readonly IMapper _mapper;

    public PreseleccionService(
        IPreseleccionRepository preseleccionRepository, 
        ISeleccionRepository seleccionRepository,
        IPeriodoConfigService periodoConfigService,
        IMapper mapper)
    {
        _preseleccionRepository = preseleccionRepository;
        _seleccionRepository = seleccionRepository;
        _periodoConfigService = periodoConfigService;
        _mapper = mapper;
    }

    private async Task<ResumenCargaDto> GetResumenCargaAsync(int usuarioId, int periodoId)
    {
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new ResumenCargaDto { MensajeEstado = "Usuario no encontrado" };

        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);
        
        // Sumar créditos de Preselección activa
        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var creditosPre = preselecciones
            .Select(p => asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == p.IdSeccionNavigation.IdAsignatura)?.Creditos ?? 0)
            .Sum();

        // Sumar créditos de Selección activa
        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var creditosSel = selecciones
            .Select(s => asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == s.IdSeccionNavigation.IdAsignatura)?.Creditos ?? 0)
            .Sum();

        int totalCreditos = creditosPre + creditosSel;
        int max = 25;
        int restantes = max - totalCreditos;

        return new ResumenCargaDto
        {
            CreditosSeleccionados = totalCreditos,
            CreditosMaximos = max,
            MensajeEstado = restantes > 0 
                ? $"Te quedan {restantes} créditos disponibles" 
                : "Has alcanzado el límite de créditos permitidos"
        };
    }

    private async Task<EstatusValidacionDto?> CheckScheduleConflictsAsync(int usuarioId, int periodoId, List<Seccion> nuevasSecciones)
    {
        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var horariosExistentes = selecciones.SelectMany(s => s.IdSeccionNavigation.SeccionHorarios).ToList();

        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        horariosExistentes.AddRange(preselecciones.SelectMany(p => p.IdSeccionNavigation.SeccionHorarios));

        return ValidateScheduleConflict(horariosExistentes, nuevasSecciones);
    }

    private EstatusValidacionDto? ValidateScheduleConflict(List<SeccionHorario> horariosExistentes, List<Seccion> nuevasSecciones)
    {
        foreach (var nuevaSeccion in nuevasSecciones)
        {
            foreach (var nuevoHorario in nuevaSeccion.SeccionHorarios)
            {
                foreach (var existente in horariosExistentes)
                {
                    // No chocar con la misma sección si ya está seleccionada/preseleccionada
                    if (nuevaSeccion.SeccionId == existente.IdSeccion) continue;

                    if (nuevoHorario.Dia == existente.Dia)
                    {
                        if (nuevoHorario.HoraInicio < existente.HoraFin && nuevoHorario.HoraFin > existente.HoraInicio)
                        {
                            return new EstatusValidacionDto
                            {
                                PuedeInscribir = false,
                                Motivo = "Choque de horario",
                                DetalleAsignatura = existente.IdSeccionNavigation.IdAsignaturaNavigation.Nombre,
                                Dia = existente.Dia.ToString(),
                                HoraInicio = existente.HoraInicio.ToString(@"hh\:mm"),
                                HoraFin = existente.HoraFin.ToString(@"hh\:mm")
                            };
                        }
                    }
                }
            }
        }

        for (int i = 0; i < nuevasSecciones.Count; i++)
        {
            for (int j = i + 1; j < nuevasSecciones.Count; j++)
            {
                foreach (var h1 in nuevasSecciones[i].SeccionHorarios)
                {
                    foreach (var h2 in nuevasSecciones[j].SeccionHorarios)
                    {
                        if (h1.Dia == h2.Dia)
                        {
                            if (h1.HoraInicio < h2.HoraFin && h1.HoraFin > h2.HoraInicio)
                            {
                                return new EstatusValidacionDto
                                {
                                    PuedeInscribir = false,
                                    Motivo = "Choque de horario entre secciones seleccionadas",
                                    DetalleAsignatura = nuevasSecciones[j].IdAsignaturaNavigation.Nombre,
                                    Dia = h1.Dia.ToString(),
                                    HoraInicio = h1.HoraInicio.ToString(@"hh\:mm"),
                                    HoraFin = h1.HoraFin.ToString(@"hh\:mm")
                                };
                            }
                        }
                    }
                }
            }
        }

        return null;
    }

    public async Task<OfertaResponseDto> GetOfertaAsync(
        int usuarioId, 
        string? searchTerm = null, 
        TipoAsignatura? tipo = null, 
        bool soloDisponibles = false, 
        ModalidadSeccion? modalidad = null,
        int? periodo = null,
        int page = 1,
        int itemsPerPage = 5)
    {
        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null) return new OfertaResponseDto();

        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new OfertaResponseDto();

        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);
        var historial = await _preseleccionRepository.GetHistorialByUsuarioAsync(usuarioId);
        var secciones = await _preseleccionRepository.GetSeccionesByPeriodoAsync(activePeriod.Codigo);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            var seccionesMatchIds = secciones
                .Where(s => s.SeccionId.ToString().Contains(searchTerm))
                .Select(s => s.IdAsignatura)
                .ToHashSet();

            asignaturasPrograma = asignaturasPrograma
                .Where(a => a.IdAsignatura.ToLower().Contains(searchTerm) || 
                            a.IdAsignaturaNavigation.Nombre.ToLower().Contains(searchTerm) ||
                            seccionesMatchIds.Contains(a.IdAsignatura))
                .ToList();
        }

        if (tipo.HasValue)
        {
            asignaturasPrograma = asignaturasPrograma
                .Where(a => a.IdAsignaturaNavigation.Tipo == tipo.Value)
                .ToList();
        }

        if (periodo.HasValue)
        {
            asignaturasPrograma = asignaturasPrograma
                .Where(a => a.Periodo == periodo.Value)
                .ToList();
        }

        if (modalidad.HasValue)
        {
            secciones = secciones.Where(s => s.Modalidad == modalidad.Value).ToList();
        }

        // Obtener secciones ya preseleccionadas o seleccionadas
        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        
        var horariosExistentes = preselecciones.SelectMany(p => p.IdSeccionNavigation.SeccionHorarios)
            .Concat(selecciones.SelectMany(s => s.IdSeccionNavigation.SeccionHorarios))
            .ToList();

        var seccionesSeleccionadasIds = preselecciones.Select(p => p.IdSeccion)
            .Concat(selecciones.Select(s => s.IdSeccion))
            .ToHashSet();

        var aprobadasIds = historial
            .Where(h => h.Estatus == HistorialEstatus.Aprobado || h.Estatus == HistorialEstatus.Convalidado || h.Estatus == HistorialEstatus.Exonerado)
            .Select(h => h.IdAsignatura)
            .ToHashSet();

        var oferta = new List<OfertaAsignaturaDto>();

        foreach (var apa in asignaturasPrograma)
        {
            if (aprobadasIds.Contains(apa.IdAsignatura)) continue;

            var faltanPrerrequisitos = apa.PreRequisitos.Any(pre => !aprobadasIds.Contains(pre));
            var esSiguientePeriodo = apa.Periodo == usuarioPrograma.TrimestreActual + 1;
            
            bool puedePreseleccionar;
            string? motivoBloqueo = null;

            if (!esSiguientePeriodo)
            {
                puedePreseleccionar = false;
                motivoBloqueo = $"La preselección solo está permitida para asignaturas del trimestre {usuarioPrograma.TrimestreActual + 1}";
            }
            else
            {
                // Es el siguiente periodo, validar prerrequisitos
                if (faltanPrerrequisitos)
                {
                    puedePreseleccionar = false;
                    motivoBloqueo = $"Bloqueada por Falta de Prerrequisitos para el Trimestre {apa.Periodo}";
                }
                else
                {
                    puedePreseleccionar = true;
                }
            }

            var seccionesAsignatura = secciones.Where(s => s.IdAsignatura == apa.IdAsignatura).ToList();
            if (!seccionesAsignatura.Any()) continue;

            var seccionesDto = _mapper.Map<List<SeccionOfertaDto>>(seccionesAsignatura);
            foreach (var sDto in seccionesDto)
            {
                sDto.Seleccionada = seccionesSeleccionadasIds.Contains(sDto.SeccionId);
                
                if (!sDto.Seleccionada)
                {
                    var seccionEntidad = seccionesAsignatura.First(s => s.SeccionId == sDto.SeccionId);
                    sDto.EstatusValidacion = ValidateScheduleConflict(horariosExistentes, new List<Seccion> { seccionEntidad });
                    
                    if (sDto.EstatusValidacion == null)
                    {
                        sDto.EstatusValidacion = new EstatusValidacionDto { PuedeInscribir = true };
                    }
                }
                else
                {
                    sDto.EstatusValidacion = new EstatusValidacionDto { PuedeInscribir = true };
                }
            }

            if (soloDisponibles)
            {
                seccionesDto = seccionesDto.Where(s => s.EstatusValidacion?.PuedeInscribir == true).ToList();
                if (!seccionesDto.Any()) continue;
            }

            var dto = new OfertaAsignaturaDto
            {
                PreseleccionId = preselecciones.FirstOrDefault(p => p.IdSeccionNavigation.IdAsignatura == apa.IdAsignatura)?.Id,
                AsignaturaId = apa.IdAsignatura,
                Asignatura = apa.IdAsignaturaNavigation.Nombre,
                Creditos = apa.Creditos,
                TipoAsignatura = apa.IdAsignaturaNavigation.Tipo.ToString(),
                PeriodoTrimestre = apa.Periodo,
                PuedePreseleccionar = puedePreseleccionar,
                MotivoBloqueo = motivoBloqueo,
                TotalSeccionesAsignatura = seccionesDto.Count,
                Secciones = seccionesDto
            };
            oferta.Add(dto);
        }

        var totalItems = oferta.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
        
        var pagedOferta = oferta
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();

        return new OfertaResponseDto
        {
            ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id),
            Oferta = pagedOferta,
            Page = page,
            ItemsPerPage = itemsPerPage,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }

    public async Task<AccionPreseleccionResponseDto> GuardarPreseleccionAsync(int usuarioId, List<int> seccionIds)
    {
        var fase = await _periodoConfigService.GetCurrentFaseAsync();
        if (fase != PeriodoFase.Preseleccion) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "El proceso de preselección no está activo actualmente." };

        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "No hay un periodo académico activo configurado." };

        // 1. Validar duplicados en la lista de entrada (IDs de sección repetidos)
        if (seccionIds.Count != seccionIds.Distinct().Count())
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = "No puedes seleccionar la misma sección más de una vez en la misma solicitud." };
        }

        // 2. Validar que ninguna de las secciones corresponda a una asignatura ya aprobada
        var historial = await _preseleccionRepository.GetHistorialByUsuarioAsync(usuarioId);
        var aprobadasIds = historial
            .Where(h => h.Estatus == HistorialEstatus.Aprobado || h.Estatus == HistorialEstatus.Convalidado || h.Estatus == HistorialEstatus.Exonerado)
            .Select(h => h.IdAsignatura)
            .ToHashSet();

        var seccionesAValidar = await _preseleccionRepository.GetSeccionesByIdsAsync(seccionIds);
        
        // 3. Validar que todas las secciones existan
        if (seccionesAValidar.Count() != seccionIds.Count)
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = "Una o más secciones seleccionadas no son válidas o no existen." };
        }

        // 4. Validar que no se intente preseleccionar la misma asignatura más de una vez (vía diferentes secciones)
        var asignaturasIds = seccionesAValidar.Select(s => s.IdAsignatura).ToList();
        if (asignaturasIds.Count != asignaturasIds.Distinct().Count())
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = "No puedes preseleccionar la misma asignatura en secciones diferentes." };
        }

        // 5. Validar prerrequisitos y nivel (Regla Evolucionada)
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new AccionPreseleccionResponseDto { Success = false, Message = "Usuario no encontrado." };
        
        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);

        foreach (var s in seccionesAValidar)
        {
            var apa = asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == s.IdAsignatura);
            if (apa != null)
            {
                var faltanPrerrequisitos = apa.PreRequisitos.Any(pre => !aprobadasIds.Contains(pre));
                var esSiguientePeriodo = apa.Periodo == usuarioPrograma.TrimestreActual + 1;

                if (!esSiguientePeriodo)
                {
                    return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes preseleccionar {s.IdAsignatura} porque solo se permiten asignaturas del trimestre {usuarioPrograma.TrimestreActual + 1}." };
                }

                if (faltanPrerrequisitos)
                {
                    return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes preseleccionar {s.IdAsignatura} porque te faltan prerrequisitos para el trimestre {apa.Periodo}." };
                }
            }
        }

        // 6. Validar choques de horario
        var conflicto = await CheckScheduleConflictsAsync(usuarioId, activePeriod.Id, seccionesAValidar.ToList());
        if (conflicto != null)
        {
            return new AccionPreseleccionResponseDto 
            { 
                Success = false, 
                Message = "Se detectó un choque de horario.",
                EstatusValidacion = conflicto,
                ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id)
            };
        }

        foreach (var s in seccionesAValidar)
        {
            if (aprobadasIds.Contains(s.IdAsignatura))
            {
                return new AccionPreseleccionResponseDto { Success = false, Message = $"La asignatura {s.IdAsignatura} ya ha sido aprobada previamente." };
            }

            if (s.CupoDisponible <= 0)
            {
                // Si ya estaba preseleccionada, no validamos cupo (ya lo tiene reservado)
                var yaPreseleccionada = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
                if (!yaPreseleccionada.Any(p => p.IdSeccion == s.SeccionId))
                {
                    return new AccionPreseleccionResponseDto { Success = false, Message = $"La sección {s.NumeroSeccion} de la asignatura {s.IdAsignatura} no tiene cupos disponibles." };
                }
            }
        }

        // 5. Validar contra registros ya activos en la base de datos
        var activasEnDb = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        
        foreach (var seccionId in seccionIds)
        {
            // Validar si la sección exacta ya está preseleccionada
            if (activasEnDb.Any(a => a.IdSeccion == seccionId))
            {
                return new AccionPreseleccionResponseDto { Success = false, Message = $"La sección con ID {seccionId} ya se encuentra registrada en tu preselección." };
            }

            // Validar si la asignatura de esa sección ya está preseleccionada (en otra sección)
            var seccionNueva = seccionesAValidar.First(s => s.SeccionId == seccionId);
            if (activasEnDb.Any(a => a.IdSeccionNavigation.IdAsignatura == seccionNueva.IdAsignatura))
            {
                return new AccionPreseleccionResponseDto { Success = false, Message = $"Ya tienes preseleccionada la asignatura {seccionNueva.IdAsignatura} en otra sección." };
            }
        }

        // 6. Validar límite de créditos
        var resumenActual = await GetResumenCargaAsync(usuarioId, activePeriod.Id);
        
        int creditosNuevos = seccionesAValidar.Sum(s => asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == s.IdAsignatura)?.Creditos ?? 0);
        
        if (resumenActual.CreditosSeleccionados + creditosNuevos > resumenActual.CreditosMaximos)
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes exceder el límite de {resumenActual.CreditosMaximos} créditos." };
        }

        // 7. Proceder a guardar (reutilizando registros inactivos si existen para evitar duplicados físicos)
        var todosLosRegistros = await _preseleccionRepository.GetByUsuarioAndPeriodoAllAsync(usuarioId, activePeriod.Id);
        
        foreach (var seccionId in seccionIds)
        {
            // Restar cupo
            var seccion = seccionesAValidar.First(s => s.SeccionId == seccionId);
            seccion.CupoDisponible--;
            await _preseleccionRepository.UpdateSeccionAsync(seccion);

            var existente = todosLosRegistros.FirstOrDefault(r => r.IdSeccion == seccionId);
            if (existente != null)
            {
                existente.Activa = true;
                existente.Procesada = false;
                existente.FechaRegistro = DateTime.Now;
                await _preseleccionRepository.UpdateAsync(existente);
            }
            else
            {
                var pre = new Preseleccion
                {
                    IdUsuario = usuarioId,
                    IdSeccion = seccionId,
                    IdPeriodo = activePeriod.Id,
                    FechaRegistro = DateTime.Now,
                    Procesada = false,
                    Activa = true
                };
                await _preseleccionRepository.AddAsync(pre);
            }
        }

        return new AccionPreseleccionResponseDto 
        { 
            Success = true, 
            Message = "Preselección guardada exitosamente.",
            ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id)
        };
    }

    public async Task<ResumenPreseleccionResponseDto> GetResumenAsync(int usuarioId)
    {
        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null) return new ResumenPreseleccionResponseDto();

        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new ResumenPreseleccionResponseDto();

        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);
        var seccionesPeriodo = await _preseleccionRepository.GetSeccionesByPeriodoAsync(activePeriod.Codigo);

        var resumen = new List<PreseleccionResumenDto>();

        foreach (var pre in preselecciones)
        {
            var seccion = pre.IdSeccionNavigation;
            var apa = asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == seccion.IdAsignatura);
            var totalSecciones = seccionesPeriodo.Count(s => s.IdAsignatura == seccion.IdAsignatura);

            var dto = new PreseleccionResumenDto
            {
                PreseleccionId = pre.Id,
                AsignaturaId = seccion.IdAsignatura,
                Asignatura = seccion.IdAsignaturaNavigation.Nombre,
                Creditos = apa?.Creditos ?? 0,
                TipoAsignatura = seccion.IdAsignaturaNavigation.Tipo.ToString(),
                PeriodoTrimestre = apa?.Periodo ?? 0,
                FechaRegistro = pre.FechaRegistro,
                TotalSeccionesAsignatura = totalSecciones,
                Secciones = new List<SeccionResumenDto> { _mapper.Map<SeccionResumenDto>(seccion) }
            };
            resumen.Add(dto);
        }

        return new ResumenPreseleccionResponseDto
        {
            ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id),
            Resumen = resumen
        };
    }

    public async Task<AccionPreseleccionResponseDto> CancelarPreseleccionAsync(int id, int usuarioId)
    {
        var pre = await _preseleccionRepository.GetByIdAsync(id);
        if (pre == null || !pre.Activa || pre.IdUsuario != usuarioId) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "No se pudo cancelar. El registro no existe, ya está inactivo o no te pertenece." };

        // Liberar cupo
        var secciones = await _preseleccionRepository.GetSeccionesByIdsAsync(new List<int> { pre.IdSeccion });
        var seccion = secciones.FirstOrDefault();
        if (seccion != null)
        {
            seccion.CupoDisponible++;
            await _preseleccionRepository.UpdateSeccionAsync(seccion);
        }

        pre.Activa = false;
        await _preseleccionRepository.UpdateAsync(pre);

        return new AccionPreseleccionResponseDto
        {
            Success = true,
            Message = "Materia cancelada de la preselección.",
            ResumenCarga = await GetResumenCargaAsync(usuarioId, pre.IdPeriodo)
        };
    }
}

public class SeleccionService : ISeleccionService
{
    private readonly ISeleccionRepository _seleccionRepository;
    private readonly IPreseleccionRepository _preseleccionRepository;
    private readonly IPeriodoConfigService _periodoConfigService;
    private readonly IMapper _mapper;

    public SeleccionService(
        ISeleccionRepository seleccionRepository, 
        IPreseleccionRepository preseleccionRepository,
        IPeriodoConfigService periodoConfigService,
        IMapper mapper)
    {
        _seleccionRepository = seleccionRepository;
        _preseleccionRepository = preseleccionRepository;
        _periodoConfigService = periodoConfigService;
        _mapper = mapper;
    }

    private async Task<ResumenCargaDto> GetResumenCargaAsync(int usuarioId, int periodoId)
    {
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new ResumenCargaDto { MensajeEstado = "Usuario no encontrado" };

        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);
        
        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var creditosPre = preselecciones
            .Select(p => asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == p.IdSeccionNavigation.IdAsignatura)?.Creditos ?? 0)
            .Sum();

        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var creditosSel = selecciones
            .Select(s => asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == s.IdSeccionNavigation.IdAsignatura)?.Creditos ?? 0)
            .Sum();

        int totalCreditos = creditosPre + creditosSel;
        int max = 25;
        int restantes = max - totalCreditos;

        return new ResumenCargaDto
        {
            CreditosSeleccionados = totalCreditos,
            CreditosMaximos = max,
            MensajeEstado = restantes > 0 
                ? $"Te quedan {restantes} créditos disponibles" 
                : "Has alcanzado el límite de créditos permitidos"
        };
    }

    private async Task<EstatusValidacionDto?> CheckScheduleConflictsAsync(int usuarioId, int periodoId, List<Seccion> nuevasSecciones)
    {
        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        var horariosExistentes = selecciones.SelectMany(s => s.IdSeccionNavigation.SeccionHorarios).ToList();

        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, periodoId);
        horariosExistentes.AddRange(preselecciones.SelectMany(p => p.IdSeccionNavigation.SeccionHorarios));

        foreach (var nuevaSeccion in nuevasSecciones)
        {
            foreach (var nuevoHorario in nuevaSeccion.SeccionHorarios)
            {
                foreach (var existente in horariosExistentes)
                {
                    // No chocar con la misma sección si ya está seleccionada/preseleccionada
                    if (nuevaSeccion.SeccionId == existente.IdSeccion) continue;

                    if (nuevoHorario.Dia == existente.Dia)
                    {
                        if (nuevoHorario.HoraInicio < existente.HoraFin && nuevoHorario.HoraFin > existente.HoraInicio)
                        {
                            return new EstatusValidacionDto
                            {
                                PuedeInscribir = false,
                                Motivo = "Choque de horario",
                                DetalleAsignatura = existente.IdSeccionNavigation.IdAsignaturaNavigation.Nombre,
                                Dia = existente.Dia.ToString(),
                                HoraInicio = existente.HoraInicio.ToString(@"hh\:mm"),
                                HoraFin = existente.HoraFin.ToString(@"hh\:mm")
                            };
                        }
                    }
                }
            }
        }

        return null;
    }

    public async Task<AccionPreseleccionResponseDto> SeleccionarAsync(int usuarioId, int seccionId)
    {
        var fase = await _periodoConfigService.GetCurrentFaseAsync();
        if (fase != PeriodoFase.Seleccion) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "El proceso de selección no está activo actualmente." };

        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null || !activePeriod.PermitirModificarEnSeleccion) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "No se permite modificar la selección en este momento." };

        // Validar si ya está seleccionada
        var actuales = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        if (actuales.Any(s => s.IdSeccion == seccionId)) 
            return new AccionPreseleccionResponseDto { Success = true, Message = "Ya tienes esta sección seleccionada.", ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id) };

        // Validar si la asignatura ya está aprobada
        var seccion = (await _preseleccionRepository.GetSeccionesByIdsAsync(new List<int> { seccionId })).FirstOrDefault();
        if (seccion == null) 
            return new AccionPreseleccionResponseDto { Success = false, Message = "Sección no encontrada." };

        var historial = await _preseleccionRepository.GetHistorialByUsuarioAsync(usuarioId);
        if (historial.Any(h => h.IdAsignatura == seccion.IdAsignatura && 
            (h.Estatus == HistorialEstatus.Aprobado || h.Estatus == HistorialEstatus.Convalidado || h.Estatus == HistorialEstatus.Exonerado)))
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = "Ya has aprobado esta asignatura." };
        }

        // Validar si ya tiene otra sección de la misma asignatura en este periodo
        if (actuales.Any(s => s.IdSeccionNavigation.IdAsignatura == seccion.IdAsignatura))
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = "Ya tienes otra sección de esta asignatura seleccionada." };
        }

        // Validar prerrequisitos y nivel (Regla Evolucionada)
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        if (usuarioPrograma == null) return new AccionPreseleccionResponseDto { Success = false, Message = "Usuario no encontrado." };

        var asignaturasPrograma = await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico);
        var apa = asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == seccion.IdAsignatura);
        
        if (apa != null)
        {
            var aprobadasIds = historial
                .Where(h => h.Estatus == HistorialEstatus.Aprobado || h.Estatus == HistorialEstatus.Convalidado || h.Estatus == HistorialEstatus.Exonerado)
                .Select(h => h.IdAsignatura)
                .ToHashSet();

            var faltanPrerrequisitos = apa.PreRequisitos.Any(pre => !aprobadasIds.Contains(pre));
            var esSiguientePeriodo = apa.Periodo == usuarioPrograma.TrimestreActual + 1;

            if (!esSiguientePeriodo)
            {
                return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes seleccionar {seccion.IdAsignatura} porque solo se permiten asignaturas del trimestre {usuarioPrograma.TrimestreActual + 1}." };
            }

            if (faltanPrerrequisitos)
            {
                return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes seleccionar {seccion.IdAsignatura} porque te faltan prerrequisitos para el trimestre {apa.Periodo}." };
            }
        }

        // Validar choques de horario
        var conflicto = await CheckScheduleConflictsAsync(usuarioId, activePeriod.Id, new List<Seccion> { seccion });
        if (conflicto != null)
        {
            return new AccionPreseleccionResponseDto 
            { 
                Success = false, 
                Message = "Se detectó un choque de horario.",
                EstatusValidacion = conflicto,
                ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id)
            };
        }

        // Validar límite de créditos
        var resumenActual = await GetResumenCargaAsync(usuarioId, activePeriod.Id);
        int creditosNuevos = asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == seccion.IdAsignatura)?.Creditos ?? 0;

        if (resumenActual.CreditosSeleccionados + creditosNuevos > resumenActual.CreditosMaximos)
        {
            return new AccionPreseleccionResponseDto { Success = false, Message = $"No puedes exceder el límite de {resumenActual.CreditosMaximos} créditos." };
        }

        var seleccion = new Seleccion
        {
            IdUsuario = usuarioId,
            IdSeccion = seccionId,
            IdPeriodo = activePeriod.Id,
            FechaConfirmacion = DateTime.Now,
            EstatusAcademico = SeleccionEstatus.Inscrito,
            VieneDePreseleccion = false
        };

        await _seleccionRepository.AddAsync(seleccion);
        
        return new AccionPreseleccionResponseDto 
        { 
            Success = true, 
            Message = "Asignatura seleccionada exitosamente.",
            ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id)
        };
    }

    public async Task<ResumenSeleccionResponseDto> GetResumenAsync(int usuarioId)
    {
        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null) return new ResumenSeleccionResponseDto();

        var selecciones = await _seleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        var usuarioPrograma = await _preseleccionRepository.GetUsuarioProgramaAsync(usuarioId);
        var asignaturasPrograma = usuarioPrograma != null 
            ? await _preseleccionRepository.GetAsignaturasByProgramaAsync(usuarioPrograma.IdProgramaAcademico)
            : new List<AsignaturaProgramaAcademico>();

        var resumenDto = _mapper.Map<List<SeleccionResumenDto>>(selecciones);
        
        foreach (var dto in resumenDto)
        {
            var seleccion = selecciones.First(s => s.IdSeccion == dto.IdSeccion);
            var apa = asignaturasPrograma.FirstOrDefault(a => a.IdAsignatura == seleccion.IdSeccionNavigation.IdAsignatura);
            dto.Creditos = apa?.Creditos ?? 0;
        }

        return new ResumenSeleccionResponseDto
        {
            ResumenCarga = await GetResumenCargaAsync(usuarioId, activePeriod.Id),
            Resumen = resumenDto
        };
    }

    public async Task<bool> ConfirmarPreseleccionAsync(int usuarioId)
    {
        var fase = await _periodoConfigService.GetCurrentFaseAsync();
        if (fase != PeriodoFase.Seleccion) return false;

        var activePeriod = await _periodoConfigService.GetActivePeriodAsync();
        if (activePeriod == null) return false;

        var preselecciones = await _preseleccionRepository.GetByUsuarioAndPeriodoAsync(usuarioId, activePeriod.Id);
        var activas = preselecciones.Where(p => p.Activa && !p.Procesada).ToList();

        if (!activas.Any()) return false;

        foreach (var pre in activas)
        {
            var seleccion = new Seleccion
            {
                IdUsuario = usuarioId,
                IdSeccion = pre.IdSeccion,
                IdPeriodo = activePeriod.Id,
                FechaConfirmacion = DateTime.Now,
                EstatusAcademico = SeleccionEstatus.Inscrito,
                VieneDePreseleccion = true
            };

            await _seleccionRepository.AddAsync(seleccion);
            
            pre.Procesada = true;
            await _preseleccionRepository.UpdateAsync(pre);
        }

        return true;
    }
}
