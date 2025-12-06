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
    }
}