using Microsoft.EntityFrameworkCore;
using SistemaAcademico.AcademicProgress.Core.Entities;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using SistemaAcademico.Persistence.Models; // Using the correct models namespace
using SistemaAcademico.Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademico.AcademicProgress.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa el repositorio para acceder a los datos de progreso académico.
    /// </summary>
    public class AcademicProgressRepository : IAcademicProgressRepository
    {
        private readonly SistemaAcademicoContext _dbContext;

        public AcademicProgressRepository(SistemaAcademicoContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GradeInfo>> GetGradesForStudentAsync(int estudianteId)
        {
            // Primero, encontrar el programa académico activo del estudiante.
            var studentProgram = await _dbContext.UsuarioProgramaAcademicos
                .FirstOrDefaultAsync(upa => upa.IdUsuario == estudianteId && upa.Estatus == "Activo");

            if (studentProgram == null)
            {
                // Si el estudiante no tiene un programa activo, no se pueden calcular los créditos.
                return Enumerable.Empty<GradeInfo>();
            }

            var query =
                from historial in _dbContext.HistorialAcademicos
                join periodo in _dbContext.PeriodoConfigs on historial.IdPeriodo equals periodo.Id
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { historial.IdAsignatura, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                where historial.IdUsuario == estudianteId
                select new GradeInfo
                {
                    Nota = historial.Calificacion,
                    Creditos = apa.Creditos,
                    PeriodoAcademico = periodo.Codigo
                };

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CourseDetailInfo>> GetGradesByPeriodAsync(int studentId, string period, IEnumerable<string> states)
        {
            var studentProgram = await _dbContext.UsuarioProgramaAcademicos
                .FirstOrDefaultAsync(upa => upa.IdUsuario == studentId && upa.Estatus == "Activo");

            if (studentProgram == null)
            {
                return Enumerable.Empty<CourseDetailInfo>();
            }

            var historyQuery =
                from historial in _dbContext.HistorialAcademicos
                join periodo in _dbContext.PeriodoConfigs on historial.IdPeriodo equals periodo.Id
                join asignatura in _dbContext.Asignaturas on historial.IdAsignatura equals asignatura.AsignaturaId
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { IdAsignatura = asignatura.AsignaturaId, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                join student in _dbContext.Usuarios on historial.IdUsuario equals student.IdUsuario
                where historial.IdUsuario == studentId
                      && periodo.Codigo == period
                      && states.Contains(historial.Estatus.ToString())
                select new CourseDetailInfo
                {
                    CourseCode = asignatura.AsignaturaId,
                    CourseName = asignatura.Nombre,
                    Credits = apa.Creditos,
                    FinalGrade = historial.Calificacion,
                    StudentName = student.Nombre + " " + student.Apellido,
                    Status = historial.Estatus.ToString()
                };

            var selectionQuery =
                from seleccion in _dbContext.Seleccions
                join seccion in _dbContext.Seccions on seleccion.IdSeccion equals seccion.SeccionId
                join periodo in _dbContext.PeriodoConfigs on seleccion.IdPeriodo equals periodo.Id
                join asignatura in _dbContext.Asignaturas on seccion.IdAsignatura equals asignatura.AsignaturaId
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { IdAsignatura = asignatura.AsignaturaId, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                join student in _dbContext.Usuarios on seleccion.IdUsuario equals student.IdUsuario
                where seleccion.IdUsuario == studentId
                      && periodo.Codigo == period
                      && states.Contains(seleccion.EstatusAcademico.ToString())
                select new CourseDetailInfo
                {
                    CourseCode = asignatura.AsignaturaId,
                    CourseName = asignatura.Nombre,
                    Credits = apa.Creditos,
                    FinalGrade = null,
                    StudentName = student.Nombre + " " + student.Apellido,
                    Status = seleccion.EstatusAcademico.ToString()
                };

            var historyResults = await historyQuery.ToListAsync();
            var selectionResults = await selectionQuery.ToListAsync();

            return historyResults.Concat(selectionResults);
        }
    }
}