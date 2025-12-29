using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class AcademicOffertData
{
    public static List<Edificio> GetEdificios() => new()
    {
        new Edificio { IdEdificio = 1, Nombre = "Ercilia Pepín", Nomenclatura = "EP" },
        new Edificio { IdEdificio = 2, Nombre = "De Ramón Picazo", Nomenclatura = "DP" },
        new Edificio { IdEdificio = 3, Nombre = "Osvaldo García de la Concha", Nomenclatura = "GC" },
        new Edificio { IdEdificio = 4, Nombre = "Eduardo Latorre – Edificio de Postgrado", Nomenclatura = "EL" },
        new Edificio { IdEdificio = 5, Nombre = "Fernando Defilló", Nomenclatura = "FD" },
        new Edificio { IdEdificio = 6, Nombre = "Evangelina Rodríguez", Nomenclatura = "ER" },
        new Edificio { IdEdificio = 7, Nombre = "Pedro Francisco Bonó", Nomenclatura = "PB" },
        new Edificio { IdEdificio = 8, Nombre = "Ana Mercedes Henríquez", Nomenclatura = "AH" },
        new Edificio { IdEdificio = 9, Nombre = "Arturo Jiménes Sabater", Nomenclatura = "AJ" }
    };

    public static List<Aula> GetAulas() => new()
    {
        new Aula { IdAula = 1, Nombre = "Aula GC-201", Capacidad = 20, IdEdificio = 3 },
        new Aula { IdAula = 2, Nombre = "Aula GC-202", Capacidad = 35, IdEdificio = 3 },
        new Aula { IdAula = 3, Nombre = "Aula GC-203", Capacidad = 20, IdEdificio = 3 },
        new Aula { IdAula = 4, Nombre = "LabTI FD-403", Capacidad = 35, IdEdificio = 5 },
        new Aula { IdAula = 5, Nombre = "LabTI FD-405", Capacidad = 35, IdEdificio = 5 }
    };

    public static List<Profesor> GetProfesors() => new()
    {
        new Profesor
        {
            IdUsuario = 1,
            GradoAcademico = "Maestría en Ciencias",
            Especialidad = "Ingeniería de Software",
            FechaContratacion = DateTime.Now,
            Estatus = EstatusProfesor.Activo,
            Bio = "Profesor con 10 años de experiencia."
        }
    };

    public static List<Seccion> GetSeccions() => new()
    {
        new Seccion
        {
            SeccionId = 1,
            IdAsignatura = "AHC109",
            NumeroSeccion = "001",
            Cupo = 30,
            CupoDisponible = 30,
            PeriodoAcademico = "2026-01",
            Estatus = EstatusSeccion.Activa,
            Modalidad = ModalidadSeccion.Presencial,
            IdProfesor = 1
        },
        new Seccion
        {
            SeccionId = 2,
            IdAsignatura = "AHO102",
            NumeroSeccion = "001",
            Cupo = 30,
            CupoDisponible = 30,
            PeriodoAcademico = "2026-01",
            Estatus = EstatusSeccion.Activa,
            Modalidad = ModalidadSeccion.Virtual,
            IdProfesor = 1
        },
        new Seccion
        {
            SeccionId = 3,
            IdAsignatura = "AHO102",
            NumeroSeccion = "002",
            Cupo = 30,
            CupoDisponible = 30,
            PeriodoAcademico = "2026-01",
            Estatus = EstatusSeccion.Activa,
            Modalidad = ModalidadSeccion.Hibrida,
            IdProfesor = 1
        }
    };

    public static List<SeccionHorario> GetSeccionHorarios() => new()
    {
        new SeccionHorario
        {
            IdSeccionHorario = 1,
            IdSeccion = 1,
            Dia = DiaSemana.Lunes,
            HoraInicio = new TimeOnly(8, 0),
            HoraFin = new TimeOnly(10, 0),
            IdAula = 1
        },
        new SeccionHorario
        {
            IdSeccionHorario = 2,
            IdSeccion = 1,
            Dia = DiaSemana.Miercoles,
            HoraInicio = new TimeOnly(8, 0),
            HoraFin = new TimeOnly(10, 0),
            IdAula = 1
        },
        new SeccionHorario
        {
            IdSeccionHorario = 3,
            IdSeccion = 2,
            Dia = DiaSemana.Martes,
            HoraInicio = new TimeOnly(14, 0),
            HoraFin = new TimeOnly(16, 0),
            IdAula = 4
        }
    };

}