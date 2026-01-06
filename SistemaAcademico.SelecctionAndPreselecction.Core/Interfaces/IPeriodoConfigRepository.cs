using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

public interface IPeriodoConfigRepository
{
    Task<PeriodoConfig?> GetActivePeriodAsync();
    Task<PeriodoConfig?> GetByIdAsync(int id);
    Task<IEnumerable<PeriodoConfig>> GetAllAsync();
    Task AddAsync(PeriodoConfig periodo);
    Task UpdateAsync(PeriodoConfig periodo);
    Task DeleteAsync(int id);
}
