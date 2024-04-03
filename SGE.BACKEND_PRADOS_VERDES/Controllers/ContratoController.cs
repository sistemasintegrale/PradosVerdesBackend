using System.Security.Claims;
using System.Security.Principal;
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
        private readonly ICuotaService _cuotaService;
        private readonly IGeneralService _generalServie;
        private IMapper _mapper;

        public ContratoController(IContratoService contratoService, IUsuarioService usuarioService, IMapper mapper, ICuotaService cuotaService, IGeneralService generalServie)
        {
            _contratoService = contratoService;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _cuotaService = cuotaService;
            _generalServie = generalServie;
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
        [HttpPost("Guardar/{guardarCuota}")]
        public async Task<ActionResult<BaseResponse<int>>> Guardar([FromBody] ContratoDTO contrato, [FromRoute] bool guardarCuota)
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
            if (data.Data > 0 && guardarCuota == true)
            {
                var cuotaAnterios = (await _cuotaService.CuotaListar(data.Data)).Data!.ToList();
                cuotaAnterios.ForEach(async x =>
                {
                    x.cntc_iusuario_crea = (int)model.cntc_iusuario_crea!;
                    x.cntc_vpc_crea = model.cntc_vpc_crea;
                    await _cuotaService.CuotaEliminar(x);
                });
                var lstCuotas = new List<Cuota>();
                if (Convert.ToInt32(model.cntc_itipo_pago) == 674) // CREDITO
                {
                    int NroCuotas = (Convert.ToInt32(model.cntc_inro_cuotas));

                    for (int y = 0; y <= NroCuotas; y++)
                    {
                        Cuota EDet = new Cuota();
                        if (y == 0)
                        {

                            EDet.cntc_inro_cuotas = y;
                            EDet.cntc_sfecha_cuota = Convert.ToDateTime(model.cntc_sfecha_contrato);
                            EDet.cntc_icod_tipo_cuota = 336;

                            EDet.cntc_nmonto_cuota = Convert.ToDecimal(model.cntc_ncuota_inicial);
                            EDet.cntc_icod_situacion = 338;

                            EDet.cntc_iusuario_crea = model.cntc_iusuario_crea;
                            EDet.cntc_vpc_crea = "WEB";
                            EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                            EDet.cntc_npagado = 0;
                            EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                            EDet.cntc_nmonto_mora = 0;
                            EDet.cntc_nmonto_mora_pago = 0;

                            lstCuotas.Add(EDet);




                        }
                        else if (y == 1)
                        {

                            EDet.cntc_inro_cuotas = y;
                            EDet.cntc_sfecha_cuota = Convert.ToDateTime(model.cntc_sfecha_contrato);
                            EDet.cntc_icod_tipo_cuota = 337;
                            EDet.cntc_nmonto_cuota = Convert.ToDecimal(model.cntc_nmonto_cuota);
                            EDet.cntc_icod_situacion = 338;
                            EDet.cntc_iusuario_crea = model.cntc_iusuario_crea;
                            EDet.cntc_vpc_crea = "WEB";
                            EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                            EDet.cntc_npagado = 0;
                            EDet.cntc_itipo_cuota = 0;
                            EDet.cntc_nmonto_mora = 0;
                            EDet.cntc_nmonto_mora_pago = 0;
                            lstCuotas.Add(EDet);


                        }
                        else
                        {

                            EDet.cntc_inro_cuotas = y;
                            EDet.cntc_sfecha_cuota = Convert.ToDateTime(model.cntc_sfecha_cuota!.Value.AddMonths(y - 1));
                            EDet.cntc_icod_tipo_cuota = 337;
                            EDet.cntc_nmonto_cuota = Convert.ToDecimal(model.cntc_nmonto_cuota);
                            EDet.cntc_icod_situacion = 338;
                            EDet.cntc_iusuario_crea = model.cntc_iusuario_crea;
                            EDet.cntc_vpc_crea = "WEB";
                            EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                            EDet.cntc_npagado = 0;
                            EDet.cntc_itipo_cuota = 0;
                            EDet.cntc_nmonto_mora = 0;
                            EDet.cntc_nmonto_mora_pago = 0;
                            lstCuotas.Add(EDet);

                        }

                    }
                }
                else
                {
                    TablaVentasDetalle obj = (await _generalServie.ListarTablaVentasDetalle(15)).Data!.ToList().Where(x => x.tabvd_iid_tabla_venta_det == 5430).FirstOrDefault()!;
                    Cuota EDetF = new Cuota();
                    EDetF.cntc_inro_cuotas = 1;
                    EDetF.cntc_sfecha_cuota = Convert.ToDateTime(model.cntc_sfecha_contrato);
                    EDetF.cntc_icod_tipo_cuota = obj.tabvd_iid_tabla_venta_det;
                    EDetF.cntc_nmonto_cuota = Convert.ToDecimal(model.cntc_nprecio_total);
                    EDetF.cntc_icod_situacion = 338;
                    EDetF.cntc_iusuario_crea = model.cntc_iusuario_crea;
                    EDetF.cntc_vpc_crea = "WEB";
                    EDetF.cntc_nsaldo = EDetF.cntc_nmonto_cuota;
                    EDetF.cntc_npagado = 0;
                    EDetF.cntc_nmonto_mora_pago = 0;
                    EDetF.cntc_itipo_cuota = 0;
                    EDetF.cntc_nmonto_mora = 0;
                    lstCuotas.Add(EDetF);
                }

                lstCuotas.ForEach(async x =>
                {
                    x.cntc_icod_contrato = data.Data;
                    x.cntc_iusuario_crea = (int)model.cntc_iusuario_crea;
                    x.cntc_vpc_crea = model.cntc_vpc_crea;
                    await _cuotaService.CuotaGuardar(x);
                });
            }
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
