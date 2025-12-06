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
            // Esta lógica asume un formato de período que se puede ordenar alfabéticamente (ej. "2023-Q3").
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
        private decimal ConvertToHonorPoints(int nota)
        {
            return nota switch
            {
                >= 97 => 4.0m,  // A+
                >= 90 => 3.75m, // A
                >= 87 => 3.5m,  // B+
                >= 80 => 3.0m,  // B
                >= 77 => 2.5m,  // C+
                >= 70 => 2.0m,  // C
                >= 60 => 1.0m,  // D
                _ => 0.0m       // F
            };
        }
    }
}