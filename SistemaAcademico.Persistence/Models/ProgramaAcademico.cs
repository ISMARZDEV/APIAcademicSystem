using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class ProgramaAcademico
{
    public int ProgramaAcademicoId { get; set; }

    public string Periodo { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public int TotalCreditos { get; set; }

    public int TrimestresMaximos { get; set; }

    public int IdCarrera { get; set; }

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioProgramaAcademico> UsuarioProgramaAcademicos { get; set; } = new List<UsuarioProgramaAcademico>();
}
