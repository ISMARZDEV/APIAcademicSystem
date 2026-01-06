namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class AccionPreseleccionResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public ResumenCargaDto ResumenCarga { get; set; } = new();
    public EstatusValidacionDto? EstatusValidacion { get; set; }
}
