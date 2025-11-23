using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class SeccionHorario
{
    public int IdSeccionHorario { get; set; }

    public string Dia { get; set; } = null!;

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdAula { get; set; }

    public virtual Aula IdAulaNavigation { get; set; } = null!;
}
