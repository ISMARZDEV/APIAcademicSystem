using System;
using SistemaAcademico.Persistence.Data;
namespace SistemaAcademico.AcademicCatalog.Core.DTOs.AcademicProgram;

public class AcademicProgramDto
{
    public int ProgramaAcademicoId { get; set; }
    public string Periodo { get; set; } = null!;
    public EstatusPrograma Estatus { get; set; }
    public int TotalCreditos { get; set; }
    public int TrimestresMaximos { get; set; }
    public int IdCarrera { get; set; }
    public string NombreCarrera { get; set; } = string.Empty;

}
