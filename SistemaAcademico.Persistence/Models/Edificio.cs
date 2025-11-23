using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Edificio
{
    public int IdEdificio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Nomenclatura { get; set; } = null!;

    public virtual ICollection<Aula> Aulas { get; set; } = new List<Aula>();
}
