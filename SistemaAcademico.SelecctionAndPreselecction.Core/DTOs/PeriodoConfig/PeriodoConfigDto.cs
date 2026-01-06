using System;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.PeriodoConfig;

public class PeriodoConfigDto
{

    public int Id { get; set; }
    public string Codigo { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public DateTime PreseleccionInicio { get; set; }
    public DateTime PreseleccionFin { get; set; }
    public DateTime SeleccionInicio { get; set; }
    public DateTime SeleccionFin { get; set; }
    public bool PermitirModificarEnSeleccion { get; set; } = true;

}
