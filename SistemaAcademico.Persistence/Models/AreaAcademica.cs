using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class AreaAcademica
{
    public string AreaAcademicaId { get; set; } = null!;

    public string AreaAcademicaNombre { get; set; } = null!;

    public virtual ICollection<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();

    public virtual ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();

    public virtual ICollection<UsuarioAreaAcademica> UsuarioAreaAcademicas { get; set; } = new List<UsuarioAreaAcademica>();
}
