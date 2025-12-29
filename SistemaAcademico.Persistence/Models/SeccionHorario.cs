using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class SeccionHorario
{
    public int IdSeccionHorario { get; set; }

    public int IdSeccion { get; set; }

    public DiaSemana Dia { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public int IdAula { get; set; }

    public virtual Aula IdAulaNavigation { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;
}
