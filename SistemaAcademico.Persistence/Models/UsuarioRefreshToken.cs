using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class UsuarioRefreshToken
{
    public int IdRefreshToken { get; set; }

    public string Token { get; set; } = null!;

    public DateOnly FechaCreacion { get; set; }

    public DateOnly FechaExpiracion { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
