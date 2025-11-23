using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Carrera
{
    public int CarreraId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Nomenclatura { get; set; } = null!;

    public string IdAreaAcademica { get; set; } = null!;

    public virtual AreaAcademica IdAreaAcademicaNavigation { get; set; } = null!;

    public virtual ICollection<ProgramaAcademico> ProgramaAcademicos { get; set; } = new List<ProgramaAcademico>();
}
