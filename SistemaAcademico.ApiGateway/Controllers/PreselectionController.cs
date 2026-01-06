using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;
using SistemaAcademico.Persistence.Data;
using SistemaAcademico.ApiGateway.Constants;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreselectionController : ControllerBase
    {
        private readonly IPreseleccionService _preseleccionService;

        public PreselectionController(IPreseleccionService preseleccionService)
        {
            _preseleccionService = preseleccionService;
        }

        [HttpGet("oferta/{usuarioId}")]
        public async Task<ActionResult<OfertaResponseDto>> GetOferta(
            int usuarioId,
            [FromQuery] string? searchTerm,
            [FromQuery] TipoAsignatura? tipoAsignatura,
            [FromQuery] bool soloDisponibles = false,
            [FromQuery] ModalidadSeccion? modalidad = null,
            [FromQuery] int? periodo = null,
            [FromQuery] int page = PaginationParams.page,
            [FromQuery] int itemsPerPage = PaginationParams.itemsPerPage)
        {
            var response = await _preseleccionService.GetOfertaAsync(usuarioId, searchTerm, tipoAsignatura, soloDisponibles, modalidad, periodo, page, itemsPerPage);
            
            if (response.TotalItems > 0 && page > response.TotalPages)
            {
                return NotFound("No hay más páginas disponibles");
            }

            return Ok(response);
        }

        [HttpPost("guardar")]
        public async Task<ActionResult<AccionPreseleccionResponseDto>> Guardar([FromBody] GuardarPreseleccionRequest request)
        {
            var result = await _preseleccionService.GuardarPreseleccionAsync(request.UsuarioId, request.SeccionIds);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("resumen/{usuarioId}")]
        public async Task<ActionResult<ResumenPreseleccionResponseDto>> GetResumen(int usuarioId)
        {
            var response = await _preseleccionService.GetResumenAsync(usuarioId);
            return Ok(response);
        }

        [HttpDelete("cancelar/{id}/{usuarioId}")]
        public async Task<ActionResult<AccionPreseleccionResponseDto>> Cancelar(int id, int usuarioId)
        {
            var result = await _preseleccionService.CancelarPreseleccionAsync(id, usuarioId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }

    public class GuardarPreseleccionRequest
    {
        public int UsuarioId { get; set; }
        public List<int> SeccionIds { get; set; } = new();
    }
}
