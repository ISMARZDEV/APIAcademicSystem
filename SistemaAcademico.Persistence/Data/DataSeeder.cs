using System;
using System.Linq;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class DataSeeder
{
    public static void SeedData(SistemaAcademicoContext appContext)
    {

        #region 1. Estructura Curricular
        /* 
            Objetivo: Definir qué materias existen y qué reglas tienen (prerrequisitos). Sin esto, el sistema no sabe si el estudiante es apto para una materia.
        */

        // Seeding de Areas Academicas
        if (!appContext.AreaAcademicas.Any())
        {
            appContext.AreaAcademicas.AddRange(CurricularStructureData.GetAreaAcademicas());
        }

        // Seeding de Carreras
        if (!appContext.Carreras.Any())
        {
            appContext.Carreras.AddRange(CurricularStructureData.GetCarreras());
        }

        // Seeding de Asignaturas 
        if (!appContext.Asignaturas.Any())
        {
            appContext.Asignaturas.AddRange(CurricularStructureData.GetAsignaturas());
        }

        // Seeding de Programa Academico 
        if (!appContext.ProgramaAcademicos.Any())
        {
            appContext.ProgramaAcademicos.AddRange(CurricularStructureData.GetProgramaAcademicos());
        }

        // Seeding de Asignatura_ProgramaAcademico 
        if (!appContext.AsignaturaProgramaAcademicos.Any())
        {
            appContext.AsignaturaProgramaAcademicos.AddRange(CurricularStructureData.GetAsignaturaProgramaAcademicos());
        }
        #endregion

        #region 2. Identidad y Acceso (Usuarios)
        /* 
            Objetivo: Crear los usuarios que van a interactuar con el sistema. Sin esto, no hay "quién" realice las acciones.
        */

        // Seeding de Roles
        if (!appContext.Rols.Any())
        {
            appContext.Rols.AddRange(IdentityAccessUsersData.GetRols());
        }

        // Seeding de Usuarios
        if (!appContext.Usuarios.Any())
        {
            appContext.Usuarios.AddRange(IdentityAccessUsersData.GetUsuarios());
        }
        #endregion

        #region 3. Oferta Académica
        /* 
            Crear las clases reales con horario. Una asignatura es solo un nombre, una Sección es la clase con profesor y hora.
        */

        // Seeding de Edificios
        if (!appContext.Edificios.Any())
        {
            appContext.Edificios.AddRange(AcademicOffertData.GetEdificios());
        }
        // Seeding de Aulas
        if (!appContext.Aulas.Any())
        {
            appContext.Aulas.AddRange(AcademicOffertData.GetAulas());
        }

        // Seeding de Profesores
        if (!appContext.Profesors.Any())
        {
            appContext.Profesors.AddRange(AcademicOffertData.GetProfesors());
        }

        // Seeding de Secciones
        if (!appContext.Seccions.Any())
        {
            appContext.Seccions.AddRange(AcademicOffertData.GetSeccions());
        }

        // Seeding de SeccionHorarios
        if (!appContext.SeccionHorarios.Any())
        {
            appContext.SeccionHorarios.AddRange(AcademicOffertData.GetSeccionHorarios());
        }

        #endregion

        #region 4. Procesos Académicos
        /* 
            Crear los procesos de preselección y selección para que los estudiantes puedan inscribirse en las clases.
        */
        // Seeding de PeriodoConfig
        if (!appContext.PeriodoConfigs.Any())
        {
            appContext.PeriodoConfigs.AddRange(AcademicProcessesData.GetPeriodoConfigs());
        }

        // Seeding de Preselecciones
        if (!appContext.Preseleccions.Any())
        {
            appContext.Preseleccions.AddRange(AcademicProcessesData.GetPreseleccions());
        }

        // Seeding de Selecciones
        if (!appContext.Seleccions.Any())
        {
            appContext.Seleccions.AddRange(AcademicProcessesData.GetSeleccions());
        }

        #endregion

        appContext.SaveChanges();
    }
}
