namespace SistemaAcademico.AcademicProgress.Core.DTOs
{
    /// <summary>
    /// DTO que representa los índices académicos de un estudiante.
    /// </summary>
    public class AcademicIndexDto
    {
        /// <summary>
        /// El índice académico del último período cursado por el estudiante.
        /// </summary>
        public decimal IndicePeriodo { get; set; }

        /// <summary>
        /// El índice académico acumulado de todos los períodos cursados.
        /// </summary>
        public decimal IndiceAcumulado { get; set; }

        /// <summary>
        /// Describe el último período utilizado para el cálculo del índice de período (ej. "2023-Q3").
        /// </summary>
        public string? UltimoPeriodo { get; set; }
    }
}