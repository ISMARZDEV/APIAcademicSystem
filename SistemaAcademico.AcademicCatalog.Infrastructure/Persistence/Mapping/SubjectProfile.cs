using System;
using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Subjects;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Asignatura, SubjectsDto>().ForMember(des => des.NombreAreaAcademica, opt => opt.MapFrom(src => src.IdAreaAcademicaNavigation.AreaAcademicaNombre)).ReverseMap();
    }
}
