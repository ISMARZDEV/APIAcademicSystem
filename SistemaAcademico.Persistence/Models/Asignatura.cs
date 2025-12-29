using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class Asignatura
{

    public string AsignaturaId { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public TipoAsignatura Tipo { get; set; }

    public string IdAreaAcademica { get; set; } = null!;

    public virtual ICollection<AsignaturaProgramaAcademico> AsignaturaProgramaAcademicoCorequisitoNavigations { get; set; } = new List<AsignaturaProgramaAcademico>();

    public virtual ICollection<AsignaturaProgramaAcademico> AsignaturaProgramaAcademicoIdAsignaturaNavigations { get; set; } = new List<AsignaturaProgramaAcademico>();

    public virtual AreaAcademica IdAreaAcademicaNavigation { get; set; } = null!;

    public virtual ICollection<Seccion> Seccions { get; set; } = new List<Seccion>();

    public virtual ICollection<Preseleccion> Preseleccions { get; set; } = new List<Preseleccion>();
}
