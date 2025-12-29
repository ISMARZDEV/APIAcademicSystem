using System;
using System.Linq;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class DataSeeder
{
    public static void SeedData(SistemaAcademicoContext appContext)
    {

        // Fase 1: El "Qué" (Estructura Curricular)
        /* 
            Objetivo: Definir qué materias existen y qué reglas tienen (prerrequisitos). Sin esto, el sistema no sabe si el estudiante es apto para una materia.
        */
        #region 
        // Seeding de Areas Academicas
        if (!appContext.AreaAcademicas.Any())
        {
            appContext.AreaAcademicas.AddRange(
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
            );
        }

        // Seeding de Carreras
        if (!appContext.Carreras.Any())
        {
            appContext.Carreras.AddRange(

            new Carrera
            {
                CarreraId = 1,
                Nombre = "Ingenieria de Software",
                Nomenclatura = "IDS",
                IdAreaAcademica = "ING"
            }

            );
        }

        // Seeding de Asignaturas 
        // // TODO: AsignaturaId debe ser de tipo string y Tipo debe ser un enum (Teoría, Laboratorio o Electiva, )
        if (!appContext.Asignaturas.Any())
        {
            appContext.Asignaturas.AddRange(

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

            );
        }

        // Seeding de Programa Academico 
        // // TODO: Estatus deberia ser un enum ("Activo", "Inactivo")
        if (!appContext.ProgramaAcademicos.Any())
        {
            appContext.ProgramaAcademicos.AddRange(

            new ProgramaAcademico
            {
                ProgramaAcademicoId = 1,
                Periodo = "2020",
                Estatus = EstatusPrograma.Activo,
                TotalCreditos = 279,
                TrimestresMaximos = 24,
                IdCarrera = 1,
            }

            );
        }

        // Seeding de Asignatura_ProgramaAcademico 
        // // TODO: Estatus deberia ser un enum ("Activo", "Inactivo")
        // // TODO: Cambie el tipo de string el IdAsignatura
        if (!appContext.AsignaturaProgramaAcademicos.Any())
        {
            appContext.AsignaturaProgramaAcademicos.AddRange(

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
            );
        }
        #endregion

        // Fase 2: El "Cuándo y Dónde" (Oferta Académica)
        /* 
            Crear las clases reales con horario. Una asignatura es solo un nombre, una Sección es la clase con profesor y hora.
        */

        // Seeding de Edificios
        if (!appContext.Edificios.Any())
        {
            appContext.Edificios.AddRange(
                new Edificio
                {
                    IdEdificio = 1,
                    Nombre = "Ercilia Pepín",
                    Nomenclatura = "EP"
                },
                new Edificio
                {
                    IdEdificio = 2,
                    Nombre = "De Ramón Picazo",
                    Nomenclatura = "DP"
                },
                new Edificio
                {
                    IdEdificio = 3,
                    Nombre = "Osvaldo García de la Concha",
                    Nomenclatura = "GC"
                },
                new Edificio
                {
                    IdEdificio = 4,
                    Nombre = "Eduardo Latorre – Edificio de Postgrado",
                    Nomenclatura = "EL"
                },
                new Edificio
                {
                    IdEdificio = 5,
                    Nombre = "Fernando Defilló",
                    Nomenclatura = "FD"
                },
                new Edificio
                {
                    IdEdificio = 6,
                    Nombre = "Evangelina Rodríguez",
                    Nomenclatura = "ER"
                },
                new Edificio
                {
                    IdEdificio = 7,
                    Nombre = "Pedro Francisco Bonó",
                    Nomenclatura = "PB"
                },
                new Edificio
                {
                    IdEdificio = 8,
                    Nombre = "Ana Mercedes Henríquez",
                    Nomenclatura = "AH"
                },
                new Edificio
                {
                    IdEdificio = 9,
                    Nombre = "Arturo Jiménes Sabater",
                    Nomenclatura = "AJ"
                }
            );
        }

        // Seeding de Aulas
        if (!appContext.Aulas.Any())
        {
            appContext.Aulas.AddRange(

                new Aula
                {
                    IdAula = 1,
                    Nombre = "Aula GC-201",
                    Capacidad = 20,
                    IdEdificio = 3
                },
                new Aula
                {
                    IdAula = 2,
                    Nombre = "Aula GC-202",
                    Capacidad = 35,
                    IdEdificio = 3
                },
                new Aula
                {
                    IdAula = 3,
                    Nombre = "Aula GC-203",
                    Capacidad = 20,
                    IdEdificio = 3
                },
                new Aula
                {
                    IdAula = 4,
                    Nombre = "LabTI FD-403",
                    Capacidad = 35,
                    IdEdificio = 5
                },
                new Aula
                {
                    IdAula = 5,
                    Nombre = "LabTI FD-405",
                    Capacidad = 35,
                    IdEdificio = 5
                }
            );
        }
        #region 

        // Seeding de Roles
        if (!appContext.Rols.Any())
        {
            appContext.Rols.AddRange(
                new Rol { RolId = 1, Descripcion = "Profesor" },
                new Rol { RolId = 2, Descripcion = "Estudiante" },
                new Rol { RolId = 3, Descripcion = "Administrador" }
            );
            appContext.SaveChanges();
        }

        // Seeding de Usuarios
        if (!appContext.Usuarios.Any())
        {
            appContext.Usuarios.AddRange(
                new Usuario
                {
                    IdUsuario = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Nacionalidad = "Dominicano",
                    Direccion = "Calle 123",
                    Telefono = "8091234567",
                    CorreoPersonal = "juan.perez@gmail.com",
                    CorreoInstitucional = "j.perez@institucion.edu.do",
                    FechaIngreso = DateOnly.FromDateTime(DateTime.Now),
                    ClaveHash = "hash",
                    IdRol = 1
                }
            );
            appContext.SaveChanges();
        }

        // Seeding de PeriodoConfig
        if (!appContext.PeriodoConfigs.Any())
        {
            appContext.PeriodoConfigs.AddRange(
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
            );
            appContext.SaveChanges();
        }

        // Seeding de Profesores
        if (!appContext.Profesors.Any())
        {
            appContext.Profesors.AddRange(
                new Profesor
                {
                    IdUsuario = 1,
                    GradoAcademico = "Maestría en Ciencias",
                    Especialidad = "Ingeniería de Software",
                    FechaContratacion = DateTime.Now,
                    Estatus = EstatusProfesor.Activo,
                    Bio = "Profesor con 10 años de experiencia."
                }
            );
            appContext.SaveChanges();
        }

        // Seeding de Secciones
        if (!appContext.Seccions.Any())
        {
            appContext.Seccions.AddRange(

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
            );
            appContext.SaveChanges();
        }

        // Seeding de SeccionHorarios
        if (!appContext.SeccionHorarios.Any())
        {
            appContext.SeccionHorarios.AddRange(
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
            );
        }

        // Seeding de Preselecciones
        if (!appContext.Preseleccions.Any())
        {
            appContext.Preseleccions.AddRange(
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
            );
        }

        // Seeding de Selecciones
        if (!appContext.Seleccions.Any())
        {
            appContext.Seleccions.AddRange(
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
            );
        }

        #endregion

        // Fase 3: El "Quién" (Usuarios y Matrícula)

        appContext.SaveChanges();
    }
}
