using System;
using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.AcademicProgram;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class AcademicProgramProfile : Profile
{
    public AcademicProgramProfile()
    {
        CreateMap<ProgramaAcademico, AcademicProgramDto>().ForMember(dest => dest.NombreCarrera, opt => opt.MapFrom(src => src.IdCarreraNavigation.Nombre)).ReverseMap();
    }
}
