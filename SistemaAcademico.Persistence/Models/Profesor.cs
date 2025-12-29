using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class Profesor
{
    public int IdUsuario { get; set; }

    public string GradoAcademico { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public DateTime FechaContratacion { get; set; }

    public EstatusProfesor Estatus { get; set; }

    public string? Bio { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Seccion> Seccions { get; set; } = new List<Seccion>();
}
