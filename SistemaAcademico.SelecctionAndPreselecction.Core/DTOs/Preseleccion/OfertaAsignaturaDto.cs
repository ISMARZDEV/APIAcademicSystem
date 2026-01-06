using System.Collections.Generic;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class OfertaAsignaturaDto
{
    public int? PreseleccionId { get; set; }
    public string AsignaturaId { get; set; } = null!;
    public string Asignatura { get; set; } = null!;
    public int Creditos { get; set; }
    public string TipoAsignatura { get; set; } = null!;
    public int PeriodoTrimestre { get; set; }
    public bool PuedePreseleccionar { get; set; }
    public string? MotivoBloqueo { get; set; }
    public int TotalSeccionesAsignatura { get; set; }
    public List<SeccionOfertaDto> Secciones { get; set; } = new();
}

public class SeccionOfertaDto
{
    public int SeccionId { get; set; }
    public string CodigoSeccion { get; set; } = null!;
    public string Profesor { get; set; } = null!;
    public int CupoTotal { get; set; }
    public int CupoDisponible { get; set; }
    public bool Seleccionada { get; set; }
    public EstatusValidacionDto? EstatusValidacion { get; set; }
    public List<HorarioOfertaDto> Horarios { get; set; } = new();
}

public class HorarioOfertaDto
{
    public string Dia { get; set; } = null!;
    public string HoraInicio { get; set; } = null!;
    public string HoraFin { get; set; } = null!;
    public string Aula { get; set; } = null!;
    public string Edificio { get; set; } = null!;
}
