using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Aula
{
    public int IdAula { get; set; }

    public string Nombre { get; set; } = null!;

    public int Capacidad { get; set; }

    public int IdEdificio { get; set; }

    public virtual Edificio IdEdificioNavigation { get; set; } = null!;

    public virtual ICollection<SeccionHorario> SeccionHorarios { get; set; } = new List<SeccionHorario>();
}
