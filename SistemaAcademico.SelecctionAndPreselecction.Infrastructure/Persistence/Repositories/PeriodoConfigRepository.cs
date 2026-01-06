using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

namespace SistemaAcademico.SelecctionAndPreselecction.Infrastructure.Persistence.Repositories;

public class PeriodoConfigRepository : IPeriodoConfigRepository
{
    private readonly SistemaAcademicoContext _context;

    public PeriodoConfigRepository(SistemaAcademicoContext context)
    {
        _context = context;
    }

    public async Task<PeriodoConfig?> GetActivePeriodAsync()
    {
        var now = DateTime.Now;
        return await _context.PeriodoConfigs
            .Where(p => now >= p.PreseleccionInicio && now <= p.SeleccionFin)
            .OrderByDescending(p => p.SeleccionFin)
            .FirstOrDefaultAsync();
    }

    public async Task<PeriodoConfig?> GetByIdAsync(int id)
    {
        return await _context.PeriodoConfigs.FindAsync(id);
    }

    public async Task<IEnumerable<PeriodoConfig>> GetAllAsync()
    {
        return await _context.PeriodoConfigs
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    public async Task AddAsync(PeriodoConfig periodo)
    {
        await _context.PeriodoConfigs.AddAsync(periodo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PeriodoConfig periodo)
    {
        _context.PeriodoConfigs.Update(periodo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var period = await _context.PeriodoConfigs.FindAsync(id);
        if (period != null)
        {
            _context.PeriodoConfigs.Remove(period);
            await _context.SaveChangesAsync();
        }
    }
}
