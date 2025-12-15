using SistemaAcademico.AcademicProgress.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademico.AcademicProgress.Core.Interfaces
{
    /// <summary>
    /// Define el contrato para el repositorio que obtiene los datos de progreso académico.
    /// </summary>
    public interface IAcademicProgressRepository
    {
        /// <summary>
        /// Obtiene todas las calificaciones de un estudiante que son válidas para el cálculo de índices.
        /// </summary>
        /// <param name="estudianteId">El ID del estudiante.</param>
        /// <returns>Una lista de objetos GradeInfo que contienen los datos de calificación y créditos.</returns>
        Task<IEnumerable<GradeInfo>> GetGradesForStudentAsync(int estudianteId);

        /// <summary>
        /// Obtiene la información detallada de las asignaturas y calificaciones de un estudiante para un período y estados específicos.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <param name="period">El período académico en formato "YYYY-Q#".</param>
        /// <param name="states">La lista de estados de la selección a incluir (ej. "Aprobando", "Cursando").</param>
        /// <returns>Una lista de objetos con la información detallada de las calificaciones.</returns>
        Task<IEnumerable<CourseDetailInfo>> GetGradesByPeriodAsync(int studentId, string period, IEnumerable<string> states);
    }
}