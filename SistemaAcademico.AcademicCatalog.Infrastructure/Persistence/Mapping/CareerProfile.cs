using System;
using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Career;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class CareerProfile : Profile
{
    public CareerProfile()
    {
        CreateMap<Carrera, CareerDto>()
            .ForMember(dest => dest.NombreAreaAcademica, opt => opt.MapFrom(src => src.IdAreaAcademicaNavigation.AreaAcademicaNombre))
            .ReverseMap();
    }
}
