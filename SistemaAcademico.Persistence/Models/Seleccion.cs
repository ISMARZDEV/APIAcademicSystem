using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Seleccion
{
    public int IdUsuario { get; set; }

    public int IdSeccion { get; set; }

    public int? Calificacion { get; set; }
<<<<<<< HEAD
     
=======

    public int? Calificacion_Mediotermino { get; set; }

>>>>>>> 637a63f671d6a0ae1fc354a3b0095e27a085a0b2
    public string PeriodoAcademico { get; set; } = null!;

    public string Asignatura { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Comentario { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
