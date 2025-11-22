using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Rol
{
    public int RolId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
