namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class EstatusValidacionDto
{
    public bool PuedeInscribir { get; set; }
    public string? Motivo { get; set; }
    public string? DetalleAsignatura { get; set; }
    public string? Dia { get; set; }
    public string? HoraInicio { get; set; }
    public string? HoraFin { get; set; }
}
