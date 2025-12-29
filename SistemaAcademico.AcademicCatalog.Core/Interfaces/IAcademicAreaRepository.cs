using System;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.Interfaces;

public interface IAcademicAreaRepository
{
    ICollection<AreaAcademica> GetAcademicsAreas();
    AreaAcademica? GetAcademicAreaById(string academicAreaId);
    bool AcademicAreaExists(string academicAreaId);
    bool AcademicAreaNameExists(string academicAreaName);
    bool CreateAcademicArea(AreaAcademica academicArea);
    bool DeleteAcademicArea(AreaAcademica academicArea);
    bool Save();
}
