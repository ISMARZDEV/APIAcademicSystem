using SistemaAcademico.AcademicProgress.Core.DTOs;
using SistemaAcademico.AcademicProgress.Core.Entities;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademico.AcademicProgress.Core.Services
{
    /// <summary>
    /// Implementa la lógica de negocio para calcular el progreso académico de un estudiante.
    /// </summary>
    public class AcademicProgressService : IAcademicProgressService
    {
        private readonly IAcademicProgressRepository _repository;

        public AcademicProgressService(IAcademicProgressRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<AcademicIndexDto?> GetAcademicIndicesAsync(int estudianteId)
        {
            if (estudianteId <= 0)
            {
                throw new ArgumentException("El ID del estudiante no es válido.", nameof(estudianteId));
            }

            var allGrades = (await _repository.GetGradesForStudentAsync(estudianteId)).ToList();

            if (!allGrades.Any())
            {
                return null; // No hay calificaciones para calcular índices.
            }

            // Ordenar por período para encontrar el más reciente.
            // Esta lógica asume un formato de período que se puede ordenar alfabéticamente (ej. "2023-T3").
            var latestPeriod = allGrades.Select(g => g.PeriodoAcademico).Distinct().OrderByDescending(p => p).FirstOrDefault();

            var gradesInLatestPeriod = allGrades.Where(g => g.PeriodoAcademico == latestPeriod).ToList();

            var indicePeriodo = CalculateWeightedAverage(gradesInLatestPeriod);
            var indiceAcumulado = CalculateWeightedAverage(allGrades);

            return new AcademicIndexDto
            {
                IndicePeriodo = indicePeriodo,
                IndiceAcumulado = indiceAcumulado,
                UltimoPeriodo = latestPeriod
            };
        }

        /// <inheritdoc />
        public async Task<GradeReportDto?> GetFinalGradesReportAsync(int studentId, int year, int trimester)
        {
            var period = $"{year}-T{trimester}"; // Corrected period format
            var statesToInclude = new[] { "Aprobado", "Reprobado", "Retirado" };
            var gradesData = await _repository.GetGradesByPeriodAsync(studentId, period, statesToInclude);

            if (!gradesData.Any()) return null;

            var report = new GradeReportDto
            {
                StudentName = gradesData.FirstOrDefault()?.StudentName ?? string.Empty,
                Period = period,
                Courses = gradesData.Select(g => new CourseGradeDto
                {
                    CourseName = g.CourseName,
                    Credits = g.Credits,
                    // NumericGrade is intentionally left null
                    // Buisness rule: only LetterGrade is provided for final grades report
                    LetterGrade = g.Status == "Retirado"
                        ? "R"
                        : g.FinalGrade.HasValue ? ConvertToLetterGrade(g.FinalGrade.Value) : null
                }).ToList()
            };

            return report;
        }

        /// <inheritdoc />
        public async Task<GradeReportDto?> GetMidtermGradesReportAsync(int studentId, int year, int trimester)
        {
            var period = $"{year}-T{trimester}"; // Corrected period format
            var statesToInclude = new[] { "Cursando", "Retirado", "Aprobado", "Reprobado" };
            var gradesData = await _repository.GetGradesByPeriodAsync(studentId, period, statesToInclude);

            if (!gradesData.Any()) return null;

            var report = new GradeReportDto
            {
                StudentName = gradesData.FirstOrDefault()?.StudentName ?? string.Empty,
                Period = period,
                Courses = gradesData.Select(g => new CourseGradeDto
                {
                    CourseName = g.CourseName,
                    Credits = g.Credits,
                    NumericGrade = null, // MidtermGrade removed from schema
                    LetterGrade = g.Status == "Retirado" ? "R" : null
                }).ToList()
            };

            return report;
        }

        private string ConvertToLetterGrade(decimal nota)
        {
            return nota switch
            {
                >= 95 => "A+",
                >= 90 => "A",
                >= 85 => "B+",
                >= 80 => "B",
                >= 75 => "C+",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }

        /// <summary>
        /// Calcula el promedio ponderado para una lista de calificaciones.
        /// </summary>
        private decimal CalculateWeightedAverage(List<GradeInfo> grades)
        {
            if (grades == null || !grades.Any())
            {
                return 0;
            }

            decimal totalPuntosHonor = 0;
            int totalCreditos = 0;

            foreach (var grade in grades)
            {
                decimal puntosHonor = ConvertToHonorPoints(grade.Nota);
                totalPuntosHonor += puntosHonor * grade.Creditos;
                totalCreditos += grade.Creditos;
            }

            if (totalCreditos == 0)
            {
                return 0;
            }

            return Math.Round(totalPuntosHonor / totalCreditos, 2);
        }

        /// <summary>
        /// Convierte una calificación numérica (0-100) a su equivalente en puntos de honor (escala 4.0).
        /// </summary>
        private decimal ConvertToHonorPoints(decimal nota)
        {
            return nota switch
            {
                >= 95 => 4.0m,  // A+
                >= 90 => 3.75m, // A
                >= 85 => 3.5m,  // B+
                >= 80 => 3.0m,  // B
                >= 75 => 2.5m,  // C+
                >= 70 => 2.0m,  // C
                >= 60 => 1.0m,  // D
                _ => 0.0m       // F
            };
        }
    }
}