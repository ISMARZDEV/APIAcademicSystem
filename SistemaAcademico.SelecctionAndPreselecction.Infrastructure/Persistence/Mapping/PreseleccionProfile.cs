using AutoMapper;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;
using System.Linq;

namespace SistemaAcademico.SelecctionAndPreselecction.Infrastructure.Persistence.Mapping;

public class PreseleccionProfile : Profile
{
    public PreseleccionProfile()
    {
        CreateMap<Seccion, SeccionOfertaDto>()
            .ForMember(dest => dest.CodigoSeccion, opt => opt.MapFrom(src => src.NumeroSeccion))
            .ForMember(dest => dest.Profesor, opt => opt.MapFrom(src => 
                src.IdProfesorNavigation != null && src.IdProfesorNavigation.IdUsuarioNavigation != null
                ? $"{src.IdProfesorNavigation.IdUsuarioNavigation.Nombre} {src.IdProfesorNavigation.IdUsuarioNavigation.Apellido}"
                : "Por asignar"))
            .ForMember(dest => dest.CupoTotal, opt => opt.MapFrom(src => src.Cupo))
            .ForMember(dest => dest.CupoDisponible, opt => opt.MapFrom(src => src.CupoDisponible))
            .ForMember(dest => dest.Horarios, opt => opt.MapFrom(src => src.SeccionHorarios));

        CreateMap<Seccion, SeccionResumenDto>()
            .ForMember(dest => dest.CodigoSeccion, opt => opt.MapFrom(src => src.NumeroSeccion))
            .ForMember(dest => dest.Profesor, opt => opt.MapFrom(src => 
                src.IdProfesorNavigation != null && src.IdProfesorNavigation.IdUsuarioNavigation != null
                ? $"{src.IdProfesorNavigation.IdUsuarioNavigation.Nombre} {src.IdProfesorNavigation.IdUsuarioNavigation.Apellido}"
                : "Por asignar"))
            .ForMember(dest => dest.CupoTotal, opt => opt.MapFrom(src => src.Cupo))
            .ForMember(dest => dest.CupoDisponible, opt => opt.MapFrom(src => src.CupoDisponible))
            .ForMember(dest => dest.Horarios, opt => opt.MapFrom(src => src.SeccionHorarios));

        CreateMap<SeccionHorario, HorarioOfertaDto>()
            .ForMember(dest => dest.Dia, opt => opt.MapFrom(src => src.Dia.ToString()))
            .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString(@"hh\:mm")))
            .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.HoraFin.ToString(@"hh\:mm")))
            .ForMember(dest => dest.Aula, opt => opt.MapFrom(src => src.IdAulaNavigation.Nombre))
            .ForMember(dest => dest.Edificio, opt => opt.MapFrom(src => 
                src.IdAulaNavigation != null && src.IdAulaNavigation.IdEdificioNavigation != null
                ? src.IdAulaNavigation.IdEdificioNavigation.Nombre
                : "N/A"));

        CreateMap<Seleccion, SeleccionResumenDto>()
            .ForMember(dest => dest.IdSeccion, opt => opt.MapFrom(src => src.IdSeccion))
            .ForMember(dest => dest.CodigoAsignatura, opt => opt.MapFrom(src => src.IdSeccionNavigation.IdAsignaturaNavigation.AsignaturaId))
            .ForMember(dest => dest.NombreAsignatura, opt => opt.MapFrom(src => src.IdSeccionNavigation.IdAsignaturaNavigation.Nombre))
            .ForMember(dest => dest.TipoAsignatura, opt => opt.MapFrom(src => src.IdSeccionNavigation.IdAsignaturaNavigation.Tipo.ToString()))
            .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.IdSeccionNavigation.NumeroSeccion))
            .ForMember(dest => dest.Profesor, opt => opt.MapFrom(src => 
                src.IdSeccionNavigation.IdProfesorNavigation != null && src.IdSeccionNavigation.IdProfesorNavigation.IdUsuarioNavigation != null
                ? $"{src.IdSeccionNavigation.IdProfesorNavigation.IdUsuarioNavigation.Nombre} {src.IdSeccionNavigation.IdProfesorNavigation.IdUsuarioNavigation.Apellido}"
                : "Por asignar"))
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.EstatusAcademico.ToString()))
            .ForMember(dest => dest.Horario, opt => opt.MapFrom(src => 
                string.Join(" | ", src.IdSeccionNavigation.SeccionHorarios.Select(h => $"{h.Dia} {h.HoraInicio:hh\\:mm}-{h.HoraFin:hh\\:mm}"))))
            .ForMember(dest => dest.AulaEdificio, opt => opt.MapFrom(src => 
                string.Join(" | ", src.IdSeccionNavigation.SeccionHorarios.Select(h => 
                    h.IdAulaNavigation != null && h.IdAulaNavigation.IdEdificioNavigation != null
                    ? $"{h.IdAulaNavigation.IdEdificioNavigation.Nombre}-{h.IdAulaNavigation.Nombre}"
                    : "N/A"))));
    }
}
