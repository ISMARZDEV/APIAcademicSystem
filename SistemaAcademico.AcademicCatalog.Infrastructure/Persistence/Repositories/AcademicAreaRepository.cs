using System;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;

public class AcademicAreaRepository : IAcademicAreaRepository
{
    private readonly SistemaAcademicoContext _db;

    public AcademicAreaRepository(SistemaAcademicoContext db)
    {
        _db = db;
    }

    public ICollection<AreaAcademica> GetAcademicsAreas()
    {
        return _db.AreaAcademicas.OrderBy(a => a.AreaAcademicaId).ToList();
    }

    public AreaAcademica? GetAcademicAreaById(string academicAreaId)
    {
        if (string.IsNullOrEmpty(academicAreaId))
        {
            return null;
        }
        return _db.AreaAcademicas.FirstOrDefault(a => a.AreaAcademicaId.ToLower().Trim() == academicAreaId.ToLower().Trim());
    }

    public bool AcademicAreaExists(string academicAreaId)
    {
        if (string.IsNullOrEmpty(academicAreaId))
        {
            return false;
        }
        return _db.AreaAcademicas.Any(a => a.AreaAcademicaId == academicAreaId);
    }

    public bool AcademicAreaNameExists(string academicAreaName)
    {
        if (string.IsNullOrEmpty(academicAreaName))
        {
            return false;
        }

        return _db.AreaAcademicas.Any(a => a.AreaAcademicaNombre.ToLower().Trim() == academicAreaName.ToLower().Trim());
    }


    public bool CreateAcademicArea(AreaAcademica academicArea)
    {
        if (academicArea == null)
        {
            return false;
        }

        _db.AreaAcademicas.Add(academicArea);
        return Save();
    }

    public bool DeleteAcademicArea(AreaAcademica academicArea)
    {
        if (academicArea == null)
        {
            return false;
        }

        _db.Remove(academicArea);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}
