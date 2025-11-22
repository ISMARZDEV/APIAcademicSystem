using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class AsignaturaProgramaAcademico
{
    public int IdAsignatura { get; set; }

    public int IdProgramaAcademico { get; set; }

    public string PreRequisitos { get; set; } = null!;

    public int Corequisito { get; set; }

    public int Creditos { get; set; }

    public virtual Asignatura CorequisitoNavigation { get; set; } = null!;

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
}
