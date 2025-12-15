using SistemaAcademico.AcademicProgress.Core.DTOs;
using System.Threading.Tasks;

namespace SistemaAcademico.AcademicProgress.Core.Interfaces
{
    /// <summary>
    /// Define el contrato para los servicios relacionados con el progreso académico de un estudiante.
    /// </summary>
    public interface IAcademicProgressService
    {
        /// <summary>
        /// Calcula y obtiene los índices académicos (de período y acumulado) para un estudiante específico.
        /// </summary>
        /// <param name="estudianteId">El ID único del estudiante.</param>
        /// <returns>
        /// Un DTO con el índice del último período y el índice acumulado.
        /// Retorna null si el estudiante no tiene calificaciones registradas.
        /// </returns>
        /// <exception cref="ArgumentException">Si el ID del estudiante es inválido.</exception>
        Task<AcademicIndexDto?> GetAcademicIndicesAsync(int estudianteId);

        /// <summary>
        /// Obtiene el reporte de calificaciones finales para un estudiante en un período específico.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <param name="year">El año del período.</param>
        /// <param name="trimester">El número del trimestre (ej. 1, 2, 3).</param>
        /// <returns>Un DTO con el reporte de calificaciones finales.</returns>
        Task<GradeReportDto?> GetFinalGradesReportAsync(int studentId, int year, int trimester);

        /// <summary>
        /// Obtiene el reporte de calificaciones de medio término para un estudiante en un período específico.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <param name="year">El año del período.</param>
        /// <param name="trimester">El número del trimestre (ej. 1, 2, 3).</param>
        /// <returns>Un DTO con el reporte de calificaciones de medio término.</returns>
        Task<GradeReportDto?> GetMidtermGradesReportAsync(int studentId, int year, int trimester);
    }
}