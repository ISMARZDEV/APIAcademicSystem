using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

namespace SistemaAcademico.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SelectionController : ControllerBase
{
    private readonly ISeleccionService _seleccionService;

    public SelectionController(ISeleccionService seleccionService)
    {
        _seleccionService = seleccionService;
    }

    [HttpGet("resumen/{usuarioId}")]
    public async Task<ActionResult<ResumenSeleccionResponseDto>> GetResumen(int usuarioId)
    {
        var response = await _seleccionService.GetResumenAsync(usuarioId);
        return Ok(response);
    }

    [HttpPost("confirmar-preseleccion/{usuarioId}")]
    public async Task<IActionResult> ConfirmarPreseleccion(int usuarioId)
    {
        var result = await _seleccionService.ConfirmarPreseleccionAsync(usuarioId);
        if (!result) return BadRequest("No se pudo confirmar la preselección. Verifique que esté en fase de selección y tenga preselecciones activas.");
        return Ok(new { message = "Preselección confirmada exitosamente." });
    }

    [HttpPost("seleccionar")]
    public async Task<ActionResult<AccionPreseleccionResponseDto>> Seleccionar([FromBody] SeleccionRequest request)
    {
        var result = await _seleccionService.SeleccionarAsync(request.UsuarioId, request.SeccionId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}

public class SeleccionRequest
{
    public int UsuarioId { get; set; }
    public int SeccionId { get; set; }
}
