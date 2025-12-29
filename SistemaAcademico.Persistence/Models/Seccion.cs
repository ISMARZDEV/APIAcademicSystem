using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.Persistence.Models;


public partial class Seccion
{
    public int SeccionId { get; set; }

    public string IdAsignatura { get; set; } = null!;

    public string NumeroSeccion { get; set; } = null!;

    public int Cupo { get; set; }

    public int CupoDisponible { get; set; }

    public string PeriodoAcademico { get; set; } = null!;

    public EstatusSeccion Estatus { get; set; }

    public ModalidadSeccion Modalidad { get; set; }

    public int IdProfesor { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual Profesor IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<SeccionHorario> SeccionHorarios { get; set; } = new List<SeccionHorario>();

    public virtual ICollection<Seleccion> Seleccions { get; set; } = new List<Seleccion>();

    public virtual ICollection<Preseleccion> Preseleccions { get; set; } = new List<Preseleccion>();
}
