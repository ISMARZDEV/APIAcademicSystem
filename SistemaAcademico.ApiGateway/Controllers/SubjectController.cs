using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicCatalog.Core.DTOs.Subjects;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.ApiGateway.Constants;
using SistemaAcademico.Persistence.Data.Responses;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectsRepository subjectsRepository, IMapper mapper)
        {
            _subjectsRepository = subjectsRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetSubjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetSubjects([FromQuery] string? search, [FromQuery] int page = PaginationParams.page, [FromQuery] int itemsPerPage = PaginationParams.itemsPerPage)
        {
            var totalSubjects = _subjectsRepository.GetTotalSubjects();
            var totalPages = (int)Math.Ceiling((double)totalSubjects / itemsPerPage);
            if (page > totalPages)
            {
                return NotFound("No hay más páginas disponibles");
            }

            var subjects = _subjectsRepository.GetSubjects(search, page, itemsPerPage);
            var subjectDto = _mapper.Map<List<SubjectsDto>>(subjects);

            var paginationResponse = new PaginationResponse<SubjectsDto>
            {
                Items = subjectDto,
                Page = page,
                ItemsPerPage = itemsPerPage,
                TotalPages = totalPages
            };
            return Ok(paginationResponse);

        }


        [HttpGet("{subjectId}", Name = "GetSubjectById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetSubjectById(string subjectId)
        {
            if (string.IsNullOrWhiteSpace(subjectId))
            {
                return BadRequest("El id de la materia no puede estar vacío.");
            }

            var subject = _subjectsRepository.GetSubjectById(subjectId);

            if (subject == null)
            {
                return NotFound($"La Subject con el id '{subjectId}' no existe.");
            }


            var subjectDto = _mapper.Map<SubjectsDto>(subject);
            return Ok(subjectDto);
        }


    }
}
