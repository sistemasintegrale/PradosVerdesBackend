using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;
using SGE.BACKEND_PRADOS_VERDES.Services;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace SGE.BACKEND_PRADOS_VERDES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;
        private readonly IUsuarioService _usuarioService;
        private IMapper _mapper;

        public ContratoController(IContratoService contratoService, IUsuarioService usuarioService, IMapper mapper)
        {
            _contratoService = contratoService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<IEnumerable<ContratoDTO>>>> GetAll([FromBody] ContratoFiltersDto filter)
        {
            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity!.Claims;
            var usuario = await _usuarioService.UsuarioGetById(Convert.ToInt32(userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value));
            filter.vendc_icod_vendedor = usuario.Data!.usua_indicador_asesor!.Value ? usuario.Data.vendc_icod_vendedor : 0;
            var data = await _contratoService.ContratoListarPorFechas(filter);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Contrato>>> GetById([FromHeader] int id)
        {
            var data = await _contratoService.ContratoGetById(id);
            return Ok(data);
        }

        [HttpGet("Funerarias")]
        public async Task<ActionResult<BaseResponse<IEnumerable<Funerarias>>>> funerarias()
        {
            var data = await _contratoService.Funerarias();
            return Ok(data);
        }

        [HttpGet("Distritos")]
        public async Task<ActionResult<BaseResponse<IEnumerable<Distrito>>>> distritos()
        {
            var data = await _contratoService.Distritos();
            return Ok(data);
        }

        [HttpGet("Parametro")]
        public async Task<ActionResult<BaseResponse<RegistroParametro>>> Parametro()
        {
            var data = await _contratoService.Parametros();
            return Ok(data);
        }

        [HttpGet("ValidarSerie/{serie}")]
        public async Task<ActionResult<BaseResponse<int>>> ValidarSerie([FromRoute] string serie)
        {
            var data = await _contratoService.ContratoValidarSerie(serie);
            return Ok(data);
        }

        [HttpPost("Guardar")]
        public async Task<ActionResult<BaseResponse<int>>> Guardar([FromBody] ContratoDTO contrato)
        {

            try
            {
                var identntity = HttpContext.User.Identity as ClaimsIdentity;
                var userclaims = identntity!.Claims;
                var usuario = await _usuarioService.UsuarioGetById(Convert.ToInt32(userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value));
                var model = _mapper.Map<Contrato>(contrato);
                model.cntc_iusuario_crea = usuario.Data!.usua_icod_usuario;
                model.cntc_iusuario_modifica = usuario.Data.usua_icod_usuario;
                model.cntc_sfecha_modifica = DateTime.Now;
                model.cntc_sfecha_crea = DateTime.Now;

                Fallecido modelFallecido = _mapper.Map<Fallecido>(contrato);
                var data = await _contratoService.ContratoGuardar(model, modelFallecido);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Eliminar/{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Eliminar([FromRoute] int id)
        {
            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity!.Claims;
            var usuario = await _usuarioService.UsuarioGetById(Convert.ToInt32(userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value));
            EliminarDTO req = new EliminarDTO();
            req.id = id;
            req.usuario = usuario.Data.usua_icod_usuario;
            var data = await _contratoService.ContratoEliminar(req);
            return Ok(data);
        }

    }
}
