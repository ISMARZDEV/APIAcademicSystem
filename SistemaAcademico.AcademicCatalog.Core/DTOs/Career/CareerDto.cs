using System;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.DTOs.Career;

public class CareerDto
{
    public int CarreraId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nomenclatura { get; set; } = null!;
    public string IdAreaAcademica { get; set; } = null!;
    public string NombreAreaAcademica { get; set; } = string.Empty;

}
