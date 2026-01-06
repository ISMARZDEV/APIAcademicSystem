using AutoMapper;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Section;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Mapping;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<Seccion, SectionDto>()
            .ForMember(dest => dest.IdSeccion, opt => opt.MapFrom(src => src.SeccionId))
            .ForMember(dest => dest.IdAsignatura, opt => opt.MapFrom(src => src.IdAsignatura))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.NumeroSeccion))
            .ForMember(dest => dest.Asignatura, opt => opt.MapFrom(src => src.IdAsignaturaNavigation.Nombre))
            .ForMember(dest => dest.Creditos, opt => opt.MapFrom(src => 
                src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.FirstOrDefault() != null 
                ? src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.First().Creditos 
                : 0))
            .ForMember(dest => dest.PreRequisitos, opt => opt.MapFrom(src => 
                src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.FirstOrDefault() != null 
                ? src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.First().PreRequisitos
                : new List<string>()))
            .ForMember(dest => dest.Corequisitos, opt => opt.MapFrom(src => 
                src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.FirstOrDefault() != null 
                ? src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.First().Corequisito 
                : null))
            .ForMember(dest => dest.Periodo, opt => opt.MapFrom(src => 
                src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.FirstOrDefault() != null 
                ? src.IdAsignaturaNavigation.AsignaturaProgramaAcademicoIdAsignaturaNavigations.First().Periodo 
                : 0))
            .ForMember(dest => dest.Profesor, opt => opt.MapFrom(src => 
                $"{src.IdProfesorNavigation.IdUsuarioNavigation.Nombre} {src.IdProfesorNavigation.IdUsuarioNavigation.Apellido}"))
            .ForMember(dest => dest.CupoTotal, opt => opt.MapFrom(src => src.Cupo))
            .ForMember(dest => dest.CupoDisponible, opt => opt.MapFrom(src => src.CupoDisponible))
            .ForMember(dest => dest.Modalidad, opt => opt.MapFrom(src => src.Modalidad.ToString()))
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => (int)src.Estatus))
            .ForMember(dest => dest.Horarios, opt => opt.MapFrom(src => src.SeccionHorarios));

        CreateMap<SeccionHorario, ScheduleDto>()
            .ForMember(dest => dest.IdHorario, opt => opt.MapFrom(src => src.IdSeccionHorario))
            .ForMember(dest => dest.Dia, opt => opt.MapFrom(src => src.Dia.ToString()))
            .ForMember(dest => dest.DiaNumero, opt => opt.MapFrom(src => (int)src.Dia + 1))
            .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString("HH:mm")))
            .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.HoraFin.ToString("HH:mm")))
            .ForMember(dest => dest.Aula, opt => opt.MapFrom(src => src.IdAulaNavigation.Nombre))
            .ForMember(dest => dest.Edificio, opt => opt.MapFrom(src => src.IdAulaNavigation.IdEdificioNavigation.Nombre));
    }
}
