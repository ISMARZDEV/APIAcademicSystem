using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class UsuarioTarifario
{
    public int IdUsuario { get; set; }

    public int IdTarifario { get; set; }

    public string Estatus { get; set; } = null!;

    public DateOnly FechaInscripcion { get; set; }

    public virtual Tarifario IdTarifarioNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
