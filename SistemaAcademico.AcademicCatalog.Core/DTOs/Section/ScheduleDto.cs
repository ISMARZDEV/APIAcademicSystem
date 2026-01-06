namespace SistemaAcademico.AcademicCatalog.Core.DTOs.Section;

public class ScheduleDto
{
    public int IdHorario { get; set; }
    public string Dia { get; set; } = null!;
    public int DiaNumero { get; set; }
    public string HoraInicio { get; set; } = null!;
    public string HoraFin { get; set; } = null!;
    public string Aula { get; set; } = null!;
    public string Edificio { get; set; } = null!;
}
