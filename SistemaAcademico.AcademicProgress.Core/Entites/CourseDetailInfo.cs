namespace SistemaAcademico.AcademicProgress.Core.Entities
{
    /// <summary>
    /// Representa una proyección de datos con la información detallada de una asignatura cursada.
    /// Esta no es una entidad de base de datos.
    /// </summary>
    public class CourseDetailInfo
    {
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int? FinalGrade { get; set; }
        public int? MidtermGrade { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Added Status property
    }
}