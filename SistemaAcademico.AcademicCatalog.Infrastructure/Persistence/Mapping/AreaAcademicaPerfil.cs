using System;
using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.AreaAcademica;
using SistemaAcademico.Persistence.Models;
namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class AreaAcademicaProfile : Profile
{
    public AreaAcademicaProfile()
    {
        CreateMap<AreaAcademica, AcademicAreaDto>().ReverseMap();
    }
}

