using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class CurricularStructureData
{
    public static List<AreaAcademica> GetAreaAcademicas() => new()
    {
        new AreaAcademica { AreaAcademicaId = "ADM", AreaAcademicaNombre = "Administración" },
        new AreaAcademica { AreaAcademicaId = "AHC", AreaAcademicaNombre = "Comunicación y Lengua" },
        new AreaAcademica { AreaAcademicaId = "AHO", AreaAcademicaNombre = "Orientación" },
        new AreaAcademica { AreaAcademicaId = "AHQ", AreaAcademicaNombre = "Investigación Científica" },
        new AreaAcademica { AreaAcademicaId = "CBA", AreaAcademicaNombre = "Medio Ambiente" },
        new AreaAcademica { AreaAcademicaId = "CBF", AreaAcademicaNombre = "Física" },
        new AreaAcademica { AreaAcademicaId = "CBM", AreaAcademicaNombre = "Matemáticas Básicas" },
        new AreaAcademica { AreaAcademicaId = "CON", AreaAcademicaNombre = "Contabilidad" },
        new AreaAcademica { AreaAcademicaId = "CSH", AreaAcademicaNombre = "Ciencias Sociales y Humanidades" },
        new AreaAcademica { AreaAcademicaId = "EAA", AreaAcademicaNombre = "Electivas de Áreas Académicas" },
        new AreaAcademica { AreaAcademicaId = "ECO", AreaAcademicaNombre = "Economía" },
        new AreaAcademica { AreaAcademicaId = "EEE", AreaAcademicaNombre = "Electivas Especializadas" },
        new AreaAcademica { AreaAcademicaId = "EFP", AreaAcademicaNombre = "Electivas Profesionalizantes" },
        new AreaAcademica { AreaAcademicaId = "ICS", AreaAcademicaNombre = "Ciberseguridad" },
        new AreaAcademica { AreaAcademicaId = "IDS", AreaAcademicaNombre = "Ingeniería de Software" },
        new AreaAcademica { AreaAcademicaId = "IEC", AreaAcademicaNombre = "Electrónica" },
        new AreaAcademica { AreaAcademicaId = "ING", AreaAcademicaNombre = "Ingeniería" },
        new AreaAcademica { AreaAcademicaId = "INS", AreaAcademicaNombre = "Computación y Sistemas" },
        new AreaAcademica { AreaAcademicaId = "ISE", AreaAcademicaNombre = "Innovación Social" },
        new AreaAcademica { AreaAcademicaId = "SHI", AreaAcademicaNombre = "Inglés" }
    };

    public static List<Carrera> GetCarreras() => new()
    {
        new Carrera
        {
            CarreraId = 1,
            Nombre = "Ingenieria de Software",
            Nomenclatura = "IDS",
            IdAreaAcademica = "ING"
        }
    };

    public static List<Asignatura> GetAsignaturas() => new()
    {
        new Asignatura
        {
            AsignaturaId = "AHC109",
            Nombre = "REDACCION",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "AHC"
        },
        new Asignatura
        {
            AsignaturaId = "AHO102",
            Nombre = "ORIENTACION ACADEMICA E INSTITUCIONAL",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "AHO"
        },
        new Asignatura
        {
            AsignaturaId = "CBA1X3",
            Nombre = "VIDA EN EL MEDIO AMBIENTE (ELECTIVAS)",
            Tipo = TipoAsignatura.Electiva,
            IdAreaAcademica = "CBA"
        },
        new Asignatura
        {
            AsignaturaId = "CBM101",
            Nombre = "ALGEBRA Y GEOMETRIA ANALITICA",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "CBM"
        },
        new Asignatura
        {
            AsignaturaId = "CSH112",
            Nombre = "CIUDADANIA Y ETICA",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "CSH"
        },
        new Asignatura
        {
            AsignaturaId = "EAA1X1",
            Nombre = "ELECTIVAS DE AREAS ACADEMICAS I",
            Tipo = TipoAsignatura.Electiva,
            IdAreaAcademica = "EAA"
        },
        new Asignatura
        {
            AsignaturaId = "EAA1X2",
            Nombre = "ELECTIVAS DE AREAS ACADEMICAS II",
            Tipo = TipoAsignatura.Electiva,
            IdAreaAcademica = "EAA"
        },
        new Asignatura
        {
            AsignaturaId = "EAA1X3",
            Nombre = "ELECTIVAS DE AREAS ACADEMICAS III - (CBA106)AMBIENTE Y CULTURA",
            Tipo = TipoAsignatura.Electiva,
            IdAreaAcademica = "EAA"
        },
        new Asignatura
        {
            AsignaturaId = "IDS207",
            Nombre = "INTRODUCCION A LA INGENIERIA DE SOFTWARE",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "IDS"
        },
        new Asignatura
        {
            AsignaturaId = "SHI103",
            Nombre = "INGLES 01 (BASICO I)",
            Tipo = TipoAsignatura.Teoría,
            IdAreaAcademica = "SHI"
        }
    };

    public static List<ProgramaAcademico> GetProgramaAcademicos() => new()
    {
        new ProgramaAcademico
        {
            ProgramaAcademicoId = 1,
            Periodo = "2020",
            Estatus = EstatusPrograma.Activo,
            TotalCreditos = 279,
            TrimestresMaximos = 24,
            IdCarrera = 1,
        }
    };

    public static List<AsignaturaProgramaAcademico> GetAsignaturaProgramaAcademicos() => new()
    {
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "AHC109",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 4
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "AHO102",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 0
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "CBA1X3",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "CBM101",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 5,
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "CSH112",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "EAA1X1",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "EAA1X2",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "EAA1X3",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2,
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "IDS207",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2,
        },
        new AsignaturaProgramaAcademico
        {
            IdAsignatura = "SHI103",
            IdProgramaAcademico = 1,
            PreRequisitos = "[]",
            Corequisito = null,
            Creditos = 2,
        }
    };

}
