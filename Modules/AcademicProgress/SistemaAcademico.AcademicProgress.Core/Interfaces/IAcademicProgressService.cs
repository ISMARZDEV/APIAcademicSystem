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
    }
}