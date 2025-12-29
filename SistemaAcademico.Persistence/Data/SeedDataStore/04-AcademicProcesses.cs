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
            Nombre = "Primer Cuatrimestre 2025",
            Codigo = "2025-01",
            PreseleccionInicio = new DateTime(2024, 11, 1),
            PreseleccionFin = new DateTime(2024, 11, 15),
            SeleccionInicio = new DateTime(2024, 12, 1),
            SeleccionFin = new DateTime(2024, 12, 20),
            PermitirModificarEnSeleccion = true
        },
        new PeriodoConfig
        {
            Id = 2,
            Nombre = "Segundo Cuatrimestre 2025",
            Codigo = "2025-02",
            PreseleccionInicio = new DateTime(2025, 3, 1),
            PreseleccionFin = new DateTime(2025, 3, 15),
            SeleccionInicio = new DateTime(2025, 4, 1),
            SeleccionFin = new DateTime(2025, 4, 20),
            PermitirModificarEnSeleccion = false
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
            Procesada = true
        },
        new Preseleccion
        {
            IdUsuario = 1,
            IdSeccion = 2,
            IdPeriodo = 1,
            FechaRegistro = DateTime.Now,
            Procesada = true
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
            Calificacion = null,
            EstatusAcademico = SeleccionEstatus.Inscrito
        },
        new Seleccion
        {
            IdUsuario = 1,
            IdSeccion = 2,
            IdPeriodo = 1,
            VieneDePreseleccion = true,
            FechaConfirmacion = DateTime.Now,
            Calificacion = null,
            EstatusAcademico = SeleccionEstatus.Inscrito
        }
    };
}
