using Microsoft.EntityFrameworkCore;
using SistemaAcademico.AcademicProgress.Core.Entities;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using SistemaAcademico.Persistence.Models; // Using the correct models namespace
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
                join seccion in _dbContext.Seccions on seleccion.IdSeccion equals seccion.Id
                // Unir con AsignaturaProgramaAcademico usando tanto el ID de la asignatura como el ID del programa del estudiante.
                join apa in _dbContext.AsignaturaProgramaAcademicos on new { seccion.IdAsignatura, IdProgramaAcademico = studentProgram.IdProgramaAcademico } equals new { apa.IdAsignatura, apa.IdProgramaAcademico }
                where seleccion.IdUsuario == estudianteId && seleccion.Calificacion.HasValue && seleccion.Estado == "Cursada"
                select new GradeInfo
                {
                    Nota = seleccion.Calificacion.Value,
                    Creditos = apa.Credito,
                    PeriodoAcademico = seleccion.PeriodoAcademico
                };

            return await query.ToListAsync();
        }
    }
}