using System;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;

public partial class HistorialAcademico
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string IdAsignatura { get; set; } = null!;

    public int IdPeriodo { get; set; }

    public decimal Calificacion { get; set; }

    public HistorialEstatus Estatus { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual PeriodoConfig IdPeriodoNavigation { get; set; } = null!;
}
