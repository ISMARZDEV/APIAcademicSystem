using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.PeriodoConfig;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoConfigController : ControllerBase
    {
        private readonly IPeriodoConfigService _periodoConfigService;

        public PeriodoConfigController(IPeriodoConfigService periodoConfigService)
        {
            _periodoConfigService = periodoConfigService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodoConfigDto>>> GetAll()
        {
            var periods = await _periodoConfigService.GetAllAsync();
            return Ok(periods);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActivePeriod()
        {
            var period = await _periodoConfigService.GetActivePeriodAsync();
            if (period == null) return NotFound("No hay un periodo activo configurado.");
            return Ok(period);
        }

        [HttpGet("fase")]
        public async Task<IActionResult> GetCurrentFase()
        {
            var fase = await _periodoConfigService.GetCurrentFaseAsync();
            return Ok(new { Fase = fase.ToString() });
        }

        [HttpGet("can-modify")]
        public async Task<IActionResult> CanModify()
        {
            var canModify = await _periodoConfigService.CanModifyAsync();
            return Ok(new { CanModify = canModify });
        }

        // 3. Crear nuevo periodo
        [HttpPost]
        public async Task<IActionResult> CreatePeriod([FromBody] CreatePeriodoConfigDto dto)
        {
            await _periodoConfigService.CreateAsync(dto);
            return Ok("Periodo creado exitosamente.");
        }

        // 4. Actualizar fechas de un periodo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePeriodDates(int id, [FromBody] CreatePeriodoConfigDto dto)
        {
            await _periodoConfigService.UpdateDatesAsync(id, dto);
            return Ok("Fechas del periodo actualizadas exitosamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriod(int id)
        {
            await _periodoConfigService.DeleteAsync(id);
            return Ok("Periodo eliminado exitosamente.");
        }

    }
}
