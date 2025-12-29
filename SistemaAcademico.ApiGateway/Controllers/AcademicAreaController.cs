using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicCatalog.Core.DTOs.AreaAcademica;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicAreaController : ControllerBase
    {
        private readonly IAcademicAreaRepository _academicAreaRepository;
        private readonly IMapper _mapper;
        public AcademicAreaController(IAcademicAreaRepository academicAreaRepository, IMapper mapper)
        {
            _academicAreaRepository = academicAreaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetAcademicArea()
        {
            var academicsAreas = _academicAreaRepository.GetAcademicsAreas();
            var academicAreaDto = _mapper.Map<List<AcademicAreaDto>>(academicsAreas);

            return Ok(academicAreaDto);
        }
        [HttpGet("{academicAreaId}", Name = "GetAcademicAreaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAcademicAreaByID(string academicAreaId)
        {
            var academicArea = _academicAreaRepository.GetAcademicAreaById(academicAreaId);
            if (academicArea == null)
            {
                return NotFound($"El AcademicArea con el id '{academicAreaId}' no existe.");
            }

            var academicAreaDto = _mapper.Map<AcademicAreaDto>(academicArea);

            return Ok(academicAreaDto);
        }

        [HttpPost(Name = "CreateAcademicArea")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult CreateAcademicArea([FromBody] AcademicAreaDto createAcademicAreaDto)
        {
            if (createAcademicAreaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_academicAreaRepository.AcademicAreaExists(createAcademicAreaDto.AreaAcademicaId))
            {
                ModelState.AddModelError("CustomError", "El AcademicAreaId ya existe");
                return BadRequest(ModelState);
            }
            if (_academicAreaRepository.AcademicAreaNameExists(createAcademicAreaDto.AreaAcademicaNombre))
            {
                ModelState.AddModelError("CustomError", "El AcademicAreaName ya existe");
                return BadRequest(ModelState);
            }
            var academicArea = _mapper.Map<AreaAcademica>(createAcademicAreaDto);
            if (!_academicAreaRepository.CreateAcademicArea(academicArea))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al guardar el registro {academicArea}");
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetAcademicAreaById", new { academicAreaId = academicArea.AreaAcademicaId }, academicArea);
        }

        [HttpDelete("{academicAreaId}", Name = "DeleteAcademicArea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAcademicArea(string academicAreaId)
        {
            var academicArea = _academicAreaRepository.GetAcademicAreaById(academicAreaId);

            if (academicArea == null)
            {
                return NotFound($"El AcademicArea con el id {academicAreaId} no existe.");
            }

            if (!_academicAreaRepository.DeleteAcademicArea(academicArea))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al eliminar el registro {academicArea}");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
