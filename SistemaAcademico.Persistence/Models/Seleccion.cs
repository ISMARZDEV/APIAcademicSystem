using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Seleccion
{
    public int IdUsuario { get; set; }

    public int IdSeccion { get; set; }

    public int? Calificacion { get; set; }

    public string PeriodoAcademico { get; set; } = null!;

    public string Asignatura { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Comentario { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
