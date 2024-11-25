using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuotaController : ControllerBase
    {
        private readonly ICuotaService _cuotaService;

        public CuotaController(ICuotaService cuotaService)
        {
            _cuotaService = cuotaService;
        }

        [HttpGet("{cntc_icod_contrato}")]
        public async Task<ActionResult<IEnumerable<Cuota>>> GetAll([FromRoute] int cntc_icod_contrato)
        {
            var data = await _cuotaService.CuotaListar(cntc_icod_contrato);
            return Ok(data);
        }


    }
}

