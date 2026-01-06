namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;

public class SeleccionResumenDto
{
    public int Id { get; set; }
    public int IdSeccion { get; set; }
    public string CodigoAsignatura { get; set; } = string.Empty;
    public string NombreAsignatura { get; set; } = string.Empty;
    public int Creditos { get; set; }
    public string TipoAsignatura { get; set; } = string.Empty;
    public string Seccion { get; set; } = string.Empty;
    public string Horario { get; set; } = string.Empty;
    public string AulaEdificio { get; set; } = string.Empty;
    public string Profesor { get; set; } = string.Empty;
    public DateTime FechaConfirmacion { get; set; }
    public string Estatus { get; set; } = string.Empty;
}
