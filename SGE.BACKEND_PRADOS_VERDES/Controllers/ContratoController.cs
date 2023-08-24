using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;
using SGE.BACKEND_PRADOS_VERDES.Services;

namespace SGE.BACKEND_PRADOS_VERDES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<IEnumerable<Contrato>>>> GetAll([FromBody] ContratoFiltersDto filter)
        {
            var data = await _contratoService.ContratoListarPorFechas(filter);
            return Ok(data);
        }

        [HttpGet("id")]
        public async Task<ActionResult<BaseResponse<Contrato>>> GetById([FromHeader]int id)
        {
            var data = await _contratoService.ContratoGetById(id);
            return Ok(data);
        }
    }
}
