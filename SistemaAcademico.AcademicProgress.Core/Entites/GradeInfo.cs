namespace SistemaAcademico.AcademicProgress.Core.Entities
{
    /// <summary>
    /// Representa una proyección de datos con la información necesaria para los cálculos de índice.
    /// Esta no es una entidad de base de datos.
    /// </summary>
    public class GradeInfo
    {
        /// <summary>
        /// Calificación numérica obtenida (0-100).
        /// </summary>
        public int Nota { get; set; }

        /// <summary>
        /// Cantidad de créditos de la asignatura.
        /// </summary>
        public int Creditos { get; set; }

        /// <summary>
        /// Período académico en que se cursó la asignatura (ej. "2023-Q3").
        /// </summary>
        public string PeriodoAcademico { get; set; } = string.Empty;
    }
}