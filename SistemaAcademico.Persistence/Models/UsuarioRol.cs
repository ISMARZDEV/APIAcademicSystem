using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class UsuarioRol
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
