using Microsoft.EntityFrameworkCore;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly SistemaAcademicoContext _db;

    public SectionRepository(SistemaAcademicoContext db)
    {
        _db = db;
    }

    public ICollection<Seccion> GetSections(string? subjectId = null)
    {
        var now = DateTime.Now;
        var activePeriod = _db.PeriodoConfigs
            .Where(p => now >= p.PreseleccionInicio && now <= p.SeleccionFin)
            .OrderByDescending(p => p.SeleccionFin)
            .FirstOrDefault();

        if (activePeriod == null)
        {
            return new List<Seccion>();
        }

        var query = _db.Seccions
            .Where(s => s.PeriodoAcademico == activePeriod.Codigo)
            .Include(s => s.IdAsignaturaNavigation)
                .ThenInclude(a => a.AsignaturaProgramaAcademicoIdAsignaturaNavigations)
            .Include(s => s.IdProfesorNavigation)
                .ThenInclude(p => p.IdUsuarioNavigation)
            .Include(s => s.SeccionHorarios)
                .ThenInclude(sh => sh.IdAulaNavigation)
                    .ThenInclude(a => a.IdEdificioNavigation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(subjectId))
        {
            query = query.Where(s => s.IdAsignatura == subjectId);
        }

        return query.ToList();
    }
}
