using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;

public class SubjectRepository : ISubjectsRepository
{
    public readonly SistemaAcademicoContext _db;

    public SubjectRepository(SistemaAcademicoContext db)
    {
        _db = db;
    }
    public ICollection<Asignatura> GetSubjects(string? search, int page, int itemsPerPage)
    {
        var query = _db.Asignaturas.Include(a => a.IdAreaAcademicaNavigation).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim().ToLower();
            query = query.Where(a => a.Nombre.ToLower().Contains(search));
        }

        return query.OrderBy(p => p.AsignaturaId).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
    }

    public int GetTotalSubjects()
    {
        return _db.Asignaturas.Count();
    }

    public Asignatura? GetSubjectById(string subjectId)
    {
        if (string.IsNullOrWhiteSpace(subjectId))
        {
            return null;
        }

        return _db.Asignaturas.Include(a => a.IdAreaAcademicaNavigation).FirstOrDefault(p => p.AsignaturaId.ToLower().Trim() == subjectId.ToLower().Trim());
    }

    public bool SubjectExists(string subjectId)
    {
        throw new NotImplementedException();
    }

    public bool SubjectNameExists(string subjectName)
    {
        throw new NotImplementedException();
    }


}
