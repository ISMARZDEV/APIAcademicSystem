using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Tarifario
{
    public int IdTarifario { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public string PeriodoAcademico { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public virtual ICollection<UsuarioTarifario> UsuarioTarifarios { get; set; } = new List<UsuarioTarifario>();
}
