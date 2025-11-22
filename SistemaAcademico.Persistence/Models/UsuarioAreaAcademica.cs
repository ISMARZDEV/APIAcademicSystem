using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class UsuarioAreaAcademica
{
    public string IdAreaAcademica { get; set; } = null!;

    public int IdUsuario { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual AreaAcademica IdAreaAcademicaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
