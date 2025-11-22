using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class UsuarioProgramaAcademico
{
    public int IdUsuario { get; set; }

    public int IdProgramaAcademico { get; set; }

    public string Estatus { get; set; } = null!;

    public DateOnly FechaInscripcion { get; set; }

    public int Permanencia { get; set; }

    public virtual ProgramaAcademico IdProgramaAcademicoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
