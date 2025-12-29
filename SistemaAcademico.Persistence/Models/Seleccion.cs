using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class Seleccion
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdSeccion { get; set; }

    public int IdPeriodo { get; set; }

    public bool VieneDePreseleccion { get; set; }

    public DateTime FechaConfirmacion { get; set; }

    public decimal? Calificacion { get; set; }

    public SeleccionEstatus EstatusAcademico { get; set; }

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual PeriodoConfig IdPeriodoNavigation { get; set; } = null!;
}
