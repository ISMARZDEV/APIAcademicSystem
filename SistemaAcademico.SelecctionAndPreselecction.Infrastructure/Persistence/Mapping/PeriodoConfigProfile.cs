using AutoMapper;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.PeriodoConfig;

namespace SistemaAcademico.SelecctionAndPreselecction.Infrastructure.Persistence.Mapping;

public class PeriodoConfigProfile : Profile
{
    public PeriodoConfigProfile()
    {
        CreateMap<PeriodoConfig, PeriodoConfigDto>();
    }
}
