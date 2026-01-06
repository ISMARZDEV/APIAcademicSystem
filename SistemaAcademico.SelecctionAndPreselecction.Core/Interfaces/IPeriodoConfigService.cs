using System.Threading.Tasks;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.PeriodoConfig;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

public enum PeriodoFase
{
    Preseleccion,
    Seleccion,
    Espera,
    Cerrado
}

public interface IPeriodoConfigService
{
    Task<IEnumerable<PeriodoConfigDto>> GetAllAsync();
    Task<PeriodoFase> GetCurrentFaseAsync();
    Task<PeriodoConfigDto?> GetActivePeriodAsync();
    Task<bool> CanModifyAsync();
    Task CreateAsync(CreatePeriodoConfigDto dto);
    Task UpdateDatesAsync(int id, CreatePeriodoConfigDto dto);
    Task DeleteAsync(int id);
}
