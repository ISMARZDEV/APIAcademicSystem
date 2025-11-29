using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Asignatura
{
    public int AsignaturaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string IdAreaAcademica { get; set; } = null!;

    public virtual ICollection<AsignaturaProgramaAcademico> AsignaturaProgramaAcademicoCorequisitoNavigations { get; set; } = new List<AsignaturaProgramaAcademico>();

    public virtual ICollection<AsignaturaProgramaAcademico> AsignaturaProgramaAcademicoIdAsignaturaNavigations { get; set; } = new List<AsignaturaProgramaAcademico>();

    public virtual AreaAcademica IdAreaAcademicaNavigation { get; set; } = null!;

    public virtual ICollection<Seccion> Seccions { get; set; } = new List<Seccion>();
}
