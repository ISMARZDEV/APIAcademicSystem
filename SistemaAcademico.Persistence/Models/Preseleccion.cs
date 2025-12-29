using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Preseleccion
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdSeccion { get; set; }

    public int IdPeriodo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool Procesada { get; set; }

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual PeriodoConfig IdPeriodoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
