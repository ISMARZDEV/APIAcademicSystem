using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.ApiGateway.Controllers
{
    [Route("api/retirement")]
    [ApiController]
    public class RetirementController : ControllerBase
    {
        private readonly SistemaAcademicoContext _context;

        public RetirementController(SistemaAcademicoContext context)
        {
            _context = context;
        }
        [HttpPut]
        public async Task<string> Retirar()
        {
            return "";
        }
    }
}
