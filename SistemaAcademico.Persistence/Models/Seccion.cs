using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Seccion
{
    public int SeccionId { get; set; }

    public int IdAsignatura { get; set; }

    public string NumeroSeccion { get; set; } = null!;

    public int Cupo { get; set; }

    public int IdProfesor { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Usuario IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Seleccion> Seleccions { get; set; } = new List<Seleccion>();
}
