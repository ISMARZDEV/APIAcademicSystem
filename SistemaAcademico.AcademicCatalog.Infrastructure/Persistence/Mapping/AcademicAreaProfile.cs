using System;
using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.AreaAcademica;
using SistemaAcademico.Persistence.Models;
namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class AcademicAreaProfile : Profile
{
    public AcademicAreaProfile()
    {
        CreateMap<AreaAcademica, AcademicAreaDto>().ReverseMap();
    }
}

