using System;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.Interfaces;

public interface IAcademicProgramRepository
{
    ICollection<ProgramaAcademico> GetAcademicsPrograms();
    ProgramaAcademico? GetAcadmicProgramById(int academicProgramId);
    bool AcademicProgramExist(int academicProgramId);
    bool AcademicProgramNameExist(int academicProgramName);

    /*
    - GetAcademicsProgramsByCareer
    → Recibe un int careerId y devuelve los AcadmicsPrograms de esa carrera en ICollection del tipo ProgramaAcademico.
    ICollection<ProgramaAcademico> GetAcademicProgramsByCareer(int careerId);
    */

    /*
    - GetAcademicProgramsByStatus
    → Recibe un int status (1: Activo, 0: Inactivo) y devuelve los AcadmicsPrograms en ICollection del tipo ProgramaAcademico.
    ICollection<ProgramaAcademico> GetAcademicProgramsByStatus(int status);
    */

}
