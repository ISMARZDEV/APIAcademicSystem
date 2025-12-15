using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/students/{studentId}/academic-progress")]
    public class AcademicProgressController : ControllerBase
    {
        private readonly IAcademicProgressService _academicProgressService;

        public AcademicProgressController(IAcademicProgressService academicProgressService)
        {
            _academicProgressService = academicProgressService;
        }

        /// <summary>
        /// Obtiene los índices académicos (de período y acumulado) para un estudiante.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <returns>Los índices académicos del estudiante.</returns>
        [HttpGet("indices")]
        public async Task<IActionResult> GetAcademicIndices(int studentId)
        {
            try
            {
                var result = await _academicProgressService.GetAcademicIndicesAsync(studentId);

                if (result == null)
                {
                    return NotFound("No se encontraron calificaciones para el estudiante especificado.");
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de calificaciones finales para un período específico.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <param name="year">El año del período (ej. 2024).</param>
        /// <param name="trimester">El número del trimestre (ej. 3).</param>
        /// <returns>Un reporte con las calificaciones finales del estudiante.</returns>
        [HttpGet("grades/final")]
        public async Task<IActionResult> GetFinalGradesReport(int studentId, [FromQuery] int year, [FromQuery] int trimester)
        {
            try
            {
                var result = await _academicProgressService.GetFinalGradesReportAsync(studentId, year, trimester);

                if (result == null)
                {
                    return NotFound("No se encontraron calificaciones finales para el período especificado.");
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de calificaciones de medio término para un período específico.
        /// </summary>
        /// <param name="studentId">El ID del estudiante.</param>
        /// <param name="year">El año del período (ej. 2024).</param>
        /// <param name="trimester">El número del trimestre (ej. 3).</param>
        /// <returns>Un reporte con las calificaciones de medio término del estudiante.</returns>
        [HttpGet("grades/midterm")]
        public async Task<IActionResult> GetMidtermGradesReport(int studentId, [FromQuery] int year, [FromQuery] int trimester)
        {
            try
            {
                var result = await _academicProgressService.GetMidtermGradesReportAsync(studentId, year, trimester);

                if (result == null)
                {
                    return NotFound("No se encontraron calificaciones de medio término para el período especificado.");
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}