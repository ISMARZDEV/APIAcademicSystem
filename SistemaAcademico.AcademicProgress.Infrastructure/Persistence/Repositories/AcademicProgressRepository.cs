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
                from seleccion in _dbContext.Seleccions
                join seccion in _dbContext.Seccions on seleccion.IdSeccion equals seccion.SeccionId
                join periodo in _dbContext.PeriodoConfigs on seleccion.IdPeriodo equals periodo.Id
                // Unir con AsignaturaProgramaAcademico usando tanto el ID de la asignatura como el ID del programa del estudiante.
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { seccion.IdAsignatura, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                where seleccion.IdUsuario == estudianteId
                      && seleccion.Calificacion.HasValue
                      && (seleccion.EstatusAcademico == SeleccionEstatus.Aprobado || seleccion.EstatusAcademico == SeleccionEstatus.Reprobado)
                select new GradeInfo
                {
                    Nota = seleccion.Calificacion.Value,
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

            var query =
                from seleccion in _dbContext.Seleccions
                join seccion in _dbContext.Seccions on seleccion.IdSeccion equals seccion.SeccionId
                join periodo in _dbContext.PeriodoConfigs on seleccion.IdPeriodo equals periodo.Id
                join asignatura in _dbContext.Asignaturas on seccion.IdAsignatura equals asignatura.AsignaturaId
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { IdAsignatura = asignatura.AsignaturaId, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                join student in _dbContext.Usuarios on seleccion.IdUsuario equals student.IdUsuario
                where seleccion.IdUsuario == studentId
                      && periodo.Codigo == period
                      && states.Contains(seleccion.EstatusAcademico.ToString()) // Filter by the provided states
                select new CourseDetailInfo
                {
                    CourseCode = asignatura.AsignaturaId.ToString(),
                    CourseName = asignatura.Nombre,
                    Credits = apa.Creditos,
                    FinalGrade = seleccion.Calificacion,
                    StudentName = student.Nombre + " " + student.Apellido,
                    Status = seleccion.EstatusAcademico.ToString() // Select the status
                };

            return await query.ToListAsync();
        }
    }
}