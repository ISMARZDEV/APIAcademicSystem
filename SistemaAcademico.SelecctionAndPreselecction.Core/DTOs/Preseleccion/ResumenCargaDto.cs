namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class ResumenCargaDto
{
    public int CreditosSeleccionados { get; set; }
    public int CreditosMaximos { get; set; } = 25;
    public bool PuedeAgregarMas => CreditosSeleccionados < CreditosMaximos;
    public string MensajeEstado { get; set; } = string.Empty;
}
