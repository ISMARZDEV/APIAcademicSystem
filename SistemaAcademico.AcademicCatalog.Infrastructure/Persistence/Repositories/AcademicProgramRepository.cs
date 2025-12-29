using System;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;

public class AcademicProgramRepository : IAcademicProgramRepository
{

    public readonly SistemaAcademicoContext _db;
    public AcademicProgramRepository(SistemaAcademicoContext db)
    {
        _db = db;
    }

    public ICollection<ProgramaAcademico> GetAcademicsPrograms()
    {
        return _db.ProgramaAcademicos.Include(p => p.IdCarreraNavigation).OrderBy(p => p.ProgramaAcademicoId).ToList();
    }

    ProgramaAcademico? IAcademicProgramRepository.GetAcadmicProgramById(int academicProgramId)
    {
        if (academicProgramId <= 0)
        {
            return null;
        }
        return _db.ProgramaAcademicos.Include(p => p.IdCarreraNavigation).FirstOrDefault(p => p.ProgramaAcademicoId == academicProgramId);
    }

    public bool AcademicProgramExist(int academicProgramId)
    {
        throw new NotImplementedException();
    }

    public bool AcademicProgramNameExist(int academicProgramName)
    {
        throw new NotImplementedException();
    }
}
