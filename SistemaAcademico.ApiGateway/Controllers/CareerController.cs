using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Career;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        public readonly ICareerRepository _careerRepository;

        public IMapper _mapper;

        public CareerController(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCareers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCareers()
        {
            var careers = _careerRepository.GetCarreers();
            var carreerDto = _mapper.Map<List<CareerDto>>(careers);
            return Ok(carreerDto);
        }

        [HttpGet("{careerId:int}", Name = "GetCareerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCareerById(int careerId)
        {
            var career = _careerRepository.GetCarreerById(careerId);
            if (career == null)
            {
                return NotFound($"La career con el id '{careerId}' no existe.");
            }
            var carreerDto = _mapper.Map<CareerDto>(career);
            return Ok(carreerDto);
        }

    }
}
