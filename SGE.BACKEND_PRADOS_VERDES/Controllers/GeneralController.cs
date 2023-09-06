using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _generalService;
        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }
        [HttpGet("TablaRegistro/{icod}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<TablaRegistro>>>> ListarTablaRegistroDetalle(int icod)
        {
            var data = await _generalService.ListarTablaRegistroDetalle(icod);
            return Ok(data);
        }

        [HttpGet("TablaVentasDetalle/{icod}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<TablaVentasDetalle>>>> ListarTablaVentasDetalle(int icod)
        {
            var data = await _generalService.ListarTablaVentasDetalle(icod);
            return Ok(data);
        }

        [HttpGet("Vendedores")]
        public async Task<ActionResult<BaseResponse<IEnumerable<Vendedor>>>> ListarVendedores()
        {
            var data = await _generalService.ListarVendedores();
            return Ok(data);
        }
       


    }
}
