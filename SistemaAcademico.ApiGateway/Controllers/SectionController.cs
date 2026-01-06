using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Section;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSections([FromQuery] string? subjectId)
        {
            var sections = _sectionRepository.GetSections(subjectId);
            var sectionsDto = _mapper.Map<List<SectionDto>>(sections);
            return Ok(sectionsDto);
        }
    }
}
