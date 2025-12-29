using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class PeriodoConfig
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public DateTime PreseleccionInicio { get; set; }

    public DateTime PreseleccionFin { get; set; }

    public DateTime SeleccionInicio { get; set; }

    public DateTime SeleccionFin { get; set; }

    public bool PermitirModificarEnSeleccion { get; set; }

    public virtual ICollection<Preseleccion> Preseleccions { get; set; } = new List<Preseleccion>();

    public virtual ICollection<Seleccion> Seleccions { get; set; } = new List<Seleccion>();
}
