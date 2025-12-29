using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoPersonal { get; set; } = null!;

    public string CorreoInstitucional { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public string ClaveHash { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual ICollection<CuentaPorPagar> CuentaPorPagars { get; set; } = new List<CuentaPorPagar>();

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Profesor? Profesor { get; set; }

    public virtual ICollection<Seleccion> Seleccions { get; set; } = new List<Seleccion>();

    public virtual ICollection<Preseleccion> Preseleccions { get; set; } = new List<Preseleccion>();

    public virtual ICollection<UsuarioAreaAcademica> UsuarioAreaAcademicas { get; set; } = new List<UsuarioAreaAcademica>();

    public virtual ICollection<UsuarioProgramaAcademico> UsuarioProgramaAcademicos { get; set; } = new List<UsuarioProgramaAcademico>();

    public virtual ICollection<UsuarioRefreshToken> UsuarioRefreshTokens { get; set; } = new List<UsuarioRefreshToken>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();

    public virtual ICollection<UsuarioTarifario> UsuarioTarifarios { get; set; } = new List<UsuarioTarifario>();
}
