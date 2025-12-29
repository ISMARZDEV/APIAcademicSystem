using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class AsignaturaProgramaAcademico
{
    public string IdAsignatura { get; set; } = null!; // TODO: Cambie el tipo a string

    public int IdProgramaAcademico { get; set; }

    public string PreRequisitos { get; set; } = null!;

    public string? Corequisito { get; set; } // TODO: Cambie el tipo a string y que sea nullable

    public int Creditos { get; set; }

    public virtual Asignatura? CorequisitoNavigation { get; set; } // TODO: Cambie el tipo a que sea nullable

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
}
