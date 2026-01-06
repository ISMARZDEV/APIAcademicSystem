using System;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.AcademicCatalog.Core.DTOs.Section;

public class SectionDto
{
    public int IdSeccion { get; set; }
    public string IdAsignatura { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public string Asignatura { get; set; } = null!;
    public int Creditos { get; set; }
    public List<string> PreRequisitos { get; set; } = new List<string>();
    public string? Corequisitos { get; set; }
    public int Periodo { get; set; }
    public string Profesor { get; set; } = null!;
    public int CupoTotal { get; set; }
    public int CupoDisponible { get; set; }
    public string Modalidad { get; set; } = null!;
    public int Estatus { get; set; }

    public ICollection<ScheduleDto> Horarios { get; set; } = new List<ScheduleDto>();
}
