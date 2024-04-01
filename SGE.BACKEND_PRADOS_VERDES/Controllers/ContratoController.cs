using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;

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
        [HttpPost("Guardar")]
        public async Task<ActionResult<BaseResponse<int>>> Guardar([FromBody] ContratoDTO contrato)
        {

            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity!.Claims;
            var usuario = await _usuarioService.UsuarioGetById(Convert.ToInt32(userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value));
            Contrato model = _mapper.Map<Contrato>(contrato);
            model.cntc_iusuario_crea = usuario.Data!.usua_icod_usuario;
            model.cntc_iusuario_modifica = usuario.Data.usua_icod_usuario;
            model.cntc_sfecha_modifica = DateTime.Now;
            model.cntc_sfecha_crea = DateTime.Now;

            Fallecido modelFallecido = _mapper.Map<Fallecido>(contrato);

            Contratante principal = new()
            {
                cntcc_iid_contratante = 1,
                cntcc_vnombre_contratante = contrato.cntc_vnombre_contratante,
                cntcc_vapellido_paterno_contratante = contrato.cntc_vapellido_paterno_contratante,
                cntcc_vapellido_materno_contratante = contrato.cntc_vapellido_materno_contratante,
                cntcc_vruc_contratante = contrato.cntc_vruc_contratante,
                cntcc_sfecha_nacimineto_contratante = contrato.cntc_sfecha_nacimineto_contratante,
                cntcc_vtelefono_contratante = contrato.cntc_vtelefono_contratante,
                cntcc_vdireccion_correo_contratante = contrato.cntc_vdireccion_correo_contratante,
                cntcc_vdireccion_contratante = contrato.cntc_vdireccion_contratante,
                cntcc_inacionalidad_contratante = contrato.cntc_inacionalidad_contratante,
                cntcc_itipo_documento_contratante = contrato.cntc_itipo_documento_contratante,
                cntcc_vdocumento_contratante = contrato.cntc_vdocumento_contratante,
                cntc_iparentesco_contratante = contrato.cntc_iparentesco_contratante,
                cntc_iestado_civil_contratante = contrato.cntc_iestado_civil_contratante,
                cntc_vparentesco_contratante = contrato.cntc_vparentesco_contratante,
                iusuario = contrato.cntc_iusuario_crea,
                vpc = "WEB"
            };

            Contratante segundo = new()
            {
                cntcc_iid_contratante = 2,
                cntcc_vnombre_contratante = contrato.cntc_vnombre_contratante2,
                cntcc_vapellido_paterno_contratante = contrato.cntc_vapellido_paterno_contratante2,
                cntcc_vapellido_materno_contratante = contrato.cntc_vapellido_materno_contratante2,
                cntcc_vruc_contratante = contrato.cntc_vruc_contratante2,
                cntcc_sfecha_nacimineto_contratante = contrato.cntc_sfecha_nacimineto_contratante2,
                cntcc_vtelefono_contratante = contrato.cntc_vtelefono_contratante2,
                cntcc_vdireccion_correo_contratante = contrato.cntc_vdireccion_correo_contratante2,
                cntcc_vdireccion_contratante = contrato.cntc_vdireccion_contratante2,
                cntcc_inacionalidad_contratante = contrato.cntc_inacionalidad_contratante2,
                cntcc_itipo_documento_contratante = contrato.cntc_itipo_documento_contratante2,
                cntcc_vdocumento_contratante = contrato.cntc_vdocumento_contratante2,
                cntc_iparentesco_contratante = contrato.cntc_iparentesco_contratante2,
                cntc_iestado_civil_contratante = contrato.cntc_iestado_civil_contratante2,
                cntc_vparentesco_contratante = contrato.cntc_vparentesco_contratante2,
                iusuario = contrato.cntc_iusuario_crea,
                vpc = "WEB"
            };

            var data = await _contratoService.ContratoGuardar(model, modelFallecido, principal, segundo);
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



        [HttpGet("Eliminar/{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Eliminar([FromRoute] int id)
        {
            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity!.Claims;
            var usuario = await _usuarioService.UsuarioGetById(Convert.ToInt32(userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value));
            EliminarDTO req = new EliminarDTO();
            req.id = id;
            req.usuario = usuario.Data!.usua_icod_usuario;
            var data = await _contratoService.ContratoEliminar(req);
            return Ok(data);
        }

        [HttpGet("ContratoImpresion/{cntc_icod_contrato}")]
        public async Task<ActionResult<BaseResponse<ContratoImpresion>>> GetContratoImpresion([FromRoute] int cntc_icod_contrato)
        {
            var data = await _contratoService.ContratoImpresion(cntc_icod_contrato);
            return Ok(data);
        }

    }
}
