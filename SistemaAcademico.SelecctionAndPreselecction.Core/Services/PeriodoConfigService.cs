using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.PeriodoConfig;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Services;

public class PeriodoConfigService : IPeriodoConfigService
{
    private readonly IPeriodoConfigRepository _periodoConfigRepository;

    public PeriodoConfigService(IPeriodoConfigRepository periodoConfigRepository)
    {
        _periodoConfigRepository = periodoConfigRepository;
    }

    public async Task<IEnumerable<PeriodoConfigDto>> GetAllAsync()
    {
        var periods = await _periodoConfigRepository.GetAllAsync();
        return periods.Select(period => new PeriodoConfigDto
        {
            Id = period.Id,
            Codigo = period.Codigo,
            Nombre = period.Nombre,
            PreseleccionInicio = period.PreseleccionInicio,
            PreseleccionFin = period.PreseleccionFin,
            SeleccionInicio = period.SeleccionInicio,
            SeleccionFin = period.SeleccionFin,
            PermitirModificarEnSeleccion = period.PermitirModificarEnSeleccion
        });
    }
    
    public async Task<PeriodoConfigDto?> GetActivePeriodAsync()
    {
        var period = await _periodoConfigRepository.GetActivePeriodAsync();
        if (period == null) return null;

        return new PeriodoConfigDto
        {
            Id = period.Id,
            Codigo = period.Codigo,
            Nombre = period.Nombre,
            PreseleccionInicio = period.PreseleccionInicio,
            PreseleccionFin = period.PreseleccionFin,
            SeleccionInicio = period.SeleccionInicio,
            SeleccionFin = period.SeleccionFin,
            PermitirModificarEnSeleccion = period.PermitirModificarEnSeleccion
        };
    }
    public async Task<PeriodoFase> GetCurrentFaseAsync()
    {
        var period = await _periodoConfigRepository.GetActivePeriodAsync();
        if (period == null) return PeriodoFase.Cerrado;

        var now = DateTime.Now;

        if (now >= period.PreseleccionInicio && now <= period.PreseleccionFin)
        {
            return PeriodoFase.Preseleccion;
        }

        if (now >= period.SeleccionInicio && now <= period.SeleccionFin)
        {
            return PeriodoFase.Seleccion;
        }

        if (now > period.PreseleccionFin && now < period.SeleccionInicio)
        {
            return PeriodoFase.Espera;
        }

        return PeriodoFase.Cerrado;
    }

    public async Task<bool> CanModifyAsync()
    {
        var period = await _periodoConfigRepository.GetActivePeriodAsync();
        if (period == null) return false;

        var fase = await GetCurrentFaseAsync();
        
        if (fase == PeriodoFase.Preseleccion) return true;
        
        if (fase == PeriodoFase.Seleccion)
        {
            return period.PermitirModificarEnSeleccion;
        }

        return false;
    }

    public async Task CreateAsync(CreatePeriodoConfigDto dto)
    {
        var period = new PeriodoConfig
        {
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            PreseleccionInicio = dto.PreseleccionInicio,
            PreseleccionFin = dto.PreseleccionFin,
            SeleccionInicio = dto.SeleccionInicio,
            SeleccionFin = dto.SeleccionFin,
            PermitirModificarEnSeleccion = dto.PermitirModificarEnSeleccion
        };

        await _periodoConfigRepository.AddAsync(period);
    }

    public async Task UpdateDatesAsync(int id, CreatePeriodoConfigDto dto)
    {
        var period = await _periodoConfigRepository.GetByIdAsync(id);
        if (period == null) return;

        period.PreseleccionInicio = dto.PreseleccionInicio;
        period.PreseleccionFin = dto.PreseleccionFin;
        period.SeleccionInicio = dto.SeleccionInicio;
        period.SeleccionFin = dto.SeleccionFin;
        period.PermitirModificarEnSeleccion = dto.PermitirModificarEnSeleccion;

        await _periodoConfigRepository.UpdateAsync(period);
    }

    public async Task DeleteAsync(int id)
    {
        await _periodoConfigRepository.DeleteAsync(id);
    }
}
