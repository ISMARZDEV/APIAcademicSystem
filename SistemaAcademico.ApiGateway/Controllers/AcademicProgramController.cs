using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.AcademicCatalog.Core.DTOs.AcademicProgram;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicProgramController : ControllerBase
    {
        public readonly IAcademicProgramRepository _academicProgramRepository;

        public readonly IMapper _mapper;

        public AcademicProgramController(IAcademicProgramRepository academicProgramRepository, IMapper mapper)
        {
            _academicProgramRepository = academicProgramRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAcademicsPrograms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetAcademicsPrograms()
        {
            var academicsPrograms = _academicProgramRepository.GetAcademicsPrograms();

            var AcademicProgramDto = _mapper.Map<List<AcademicProgramDto>>(academicsPrograms);

            return Ok(AcademicProgramDto);
        }

        [HttpGet("{academicProgramId:int}:", Name = "GetAcademicProgramById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAcademicProgramById(int academicProgramId)
        {
            var academicProgram = _academicProgramRepository.GetAcadmicProgramById(academicProgramId);
            if (academicProgram == null)
            {
                return NotFound($"El AcademicProgram con el id '{academicProgramId}' no existe.");
            }
            var AcademicProgramDto = _mapper.Map<AcademicProgramDto>(academicProgram);

            return Ok(AcademicProgramDto);
        }

    }
}
