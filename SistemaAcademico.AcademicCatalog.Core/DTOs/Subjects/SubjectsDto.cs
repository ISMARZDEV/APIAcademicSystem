using System;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.AcademicCatalog.Core.DTOs.Subjects;

public class SubjectsDto
{
    public string AsignaturaId { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public TipoAsignatura Tipo { get; set; }
    public string IdAreaAcademica { get; set; } = null!;
    public string NombreAreaAcademica { get; set; } = string.Empty;
}