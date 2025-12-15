using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaAcademico.AcademicProgress.Core.DTOs
{
    /// <summary>
    /// Representa un reporte de calificaciones para un período específico.
    /// </summary>
    public class GradeReportDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public List<CourseGradeDto> Courses { get; set; } = new List<CourseGradeDto>();
    }

    /// <summary>
    /// Representa la calificación de una asignatura individual dentro de un reporte.
    /// </summary>
    public class CourseGradeDto
    {
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        //Uncomment the following lines if you want to ignore null values during JSON serialization
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NumericGrade { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LetterGrade { get; set; }
    }
}