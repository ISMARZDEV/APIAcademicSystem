using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class AcademicProcessesData
{
    public static List<PeriodoConfig> GetPeriodoConfigs() => new()
    {
        new PeriodoConfig
        {
            Id = 1,
            Nombre = "Primer Trimestre 2026",
            Codigo = "2026-01",
            PreseleccionInicio = new DateTime(2025, 12, 25),
            PreseleccionFin = new DateTime(2026, 1, 15),
            SeleccionInicio = new DateTime(2026, 1, 16),
            SeleccionFin = new DateTime(2026, 1, 20),
            PermitirModificarEnSeleccion = true
        }
    };

    public static List<Preseleccion> GetPreseleccions() => new()
    {
        new Preseleccion
        {
            IdUsuario = 1,
            IdSeccion = 1,
            IdPeriodo = 1,
            FechaRegistro = DateTime.Now,
            Procesada = true,
            Activa = false
        },
        new Preseleccion
        {
            IdUsuario = 1,
            IdSeccion = 2,
            IdPeriodo = 1,
            FechaRegistro = DateTime.Now,
            Procesada = true,
            Activa = false
        }
    };

    public static List<Seleccion> GetSeleccions() => new()
    {
        new Seleccion
        {
            IdUsuario = 1,
            IdSeccion = 1,
            IdPeriodo = 1,
            VieneDePreseleccion = true,
            FechaConfirmacion = DateTime.Now,
            EstatusAcademico = SeleccionEstatus.Inscrito
        },
        new Seleccion
        {
            IdUsuario = 1,
            IdSeccion = 2,
            IdPeriodo = 1,
            VieneDePreseleccion = true,
            FechaConfirmacion = DateTime.Now,
            EstatusAcademico = SeleccionEstatus.Inscrito
        }
    };

    public static List<UsuarioProgramaAcademico> GetUsuarioProgramaAcademicos() => new()
    {
        new UsuarioProgramaAcademico
        {
            IdUsuario = 2, // Maria Gomez
            IdProgramaAcademico = 1, // IDS 2020
            FechaInscripcion = DateOnly.FromDateTime(new DateTime(2024, 1, 10)),
            Estatus = "Activo",
            TrimestreActual = 1
        }
    };

    public static List<HistorialAcademico> GetHistorialAcademicos() => new()
    {
        new HistorialAcademico
        {
            IdUsuario = 2,
            IdAsignatura = "AHO102", // Orientación
            IdPeriodo = 1,
            Calificacion = 95,
            Estatus = HistorialEstatus.Aprobado,
            FechaRegistro = DateTime.Now.AddMonths(-6)
        },
        new HistorialAcademico
        {
            IdUsuario = 2,
            IdAsignatura = "CBA1X3", // Vida en el medio ambiente
            IdPeriodo = 1,
            Calificacion = 88,
            Estatus = HistorialEstatus.Aprobado,
            FechaRegistro = DateTime.Now.AddMonths(-6)
        }
    };
}
