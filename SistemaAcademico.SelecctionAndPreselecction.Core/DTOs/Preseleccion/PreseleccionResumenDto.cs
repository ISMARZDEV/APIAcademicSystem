using System;
using System.Collections.Generic;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class PreseleccionResumenDto
{
    public int PreseleccionId { get; set; }
    public string AsignaturaId { get; set; } = null!;
    public string Asignatura { get; set; } = null!;
    public int Creditos { get; set; }
    public string TipoAsignatura { get; set; } = null!;
    public int PeriodoTrimestre { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int TotalSeccionesAsignatura { get; set; }
    public List<SeccionResumenDto> Secciones { get; set; } = new();
}

public class SeccionResumenDto
{
    public int SeccionId { get; set; }
    public string CodigoSeccion { get; set; } = null!;
    public string Profesor { get; set; } = null!;
    public int CupoTotal { get; set; }
    public int CupoDisponible { get; set; }
    public List<HorarioOfertaDto> Horarios { get; set; } = new();
}
