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
    }
}