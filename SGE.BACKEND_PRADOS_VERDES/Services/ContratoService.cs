using AutoMapper;
using Dapper;
using SGE.BACKEND_PRADOS_VERDES.Context;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;
using System.Data;

namespace SGE.BACKEND_PRADOS_VERDES.Services
{
    public class ContratoService : IContratoService
    {
        private readonly Conexion _conexion;
        private IMapper _mapper;

        public ContratoService(Conexion conexion, IMapper mapper)
        {
            _conexion = conexion;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> ContratoEliminar(EliminarDTO dTO)
        {
            var response = new BaseResponse<bool>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    await conexion.ExecuteAsync("USP_CONTRATO_ELIMINAR", dTO, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<Contrato>> ContratoGetById(int cntc_icod_contrato)
        {
            var response = new BaseResponse<Contrato>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryFirstAsync<Contrato>($"SELECT * FROM SGE_CONTRATOS WHERE {nameof(cntc_icod_contrato)} = {cntc_icod_contrato}");
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }


        public async Task<BaseResponse<int>> ContratoGuardar(Contrato contrato, Fallecido fallecido, Contratante pricipal, Contratante secundario)
        {
            var response = new BaseResponse<int>();
            try
            {
                var validarSerie = (await ContratoValidarSerie(contrato.cntc_vnumero_contrato!)).Data;
                if (validarSerie == 1 && contrato.cntc_icod_contrato == 0)
                {
                    throw new ArgumentException($"Ya existe contrato N° {contrato.cntc_vnumero_contrato!}");
                }

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.ExecuteScalarAsync<int>("USP_CONTRATO_GUARDAR", contrato, commandType: CommandType.StoredProcedure);
                }
                fallecido.cntc_icod_contrato = response.Data;
                fallecido.usuario_accion = (int)(contrato.cntc_iusuario_crea ?? contrato.cntc_iusuario_modifica)!;
                fallecido.pc_accion = (string.IsNullOrEmpty(contrato.cntc_vpc_crea) ? contrato.cntc_vpc_crea : contrato.cntc_vpc_modifica)!;
                fallecido.cntc_sfecha_accion = DateTime.Now;

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    await conexion.ExecuteScalarAsync<int>("USP_CONTRATOS_FALLECIDO_GUARDAR", fallecido, commandType: CommandType.StoredProcedure);
                }

                pricipal.cntc_icod_contrato = response.Data;
                secundario.cntc_icod_contrato = response.Data;

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    await conexion.ExecuteAsync("USP_CONTRATANTES_INSERTAR", pricipal, commandType: CommandType.StoredProcedure);
                }

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    await conexion.ExecuteAsync("USP_CONTRATANTES_INSERTAR", secundario, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<ContratoImpresion>> ContratoImpresion(int cntc_icod_contrato)
        {
            var response = new BaseResponse<ContratoImpresion>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@cntc_icod_contrato", cntc_icod_contrato);
                    response.Data = await conexion.QueryFirstOrDefaultAsync<ContratoImpresion>("USP_CONTRATO_IMPRESION_POR_ID", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.InnerException;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<ContratoDTO>>> ContratoListarPorFechas(ContratoFiltersDto filter)
        {
            var response = new BaseResponse<IEnumerable<ContratoDTO>>();
            try
            {
                var param = _mapper.Map<ContratoFilter>(filter);
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<ContratoDTO>("USP_CONTRATO_LISTAR_POR_FECHA", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }



        public async Task<BaseResponse<int>> ContratoValidarSerie(string serie)
        {
            var response = new BaseResponse<int>();
            try
            {

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@serie", serie);
                    response.Data = await conexion.ExecuteScalarAsync<int>("USP_CONTRATO_VALIDAR_SERIE", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Distrito>>> Distritos()
        {
            var response = new BaseResponse<IEnumerable<Distrito>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<Distrito>("SGEV_DISTRITOS_LISTAR", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Funerarias>>> Funerarias()
        {
            var response = new BaseResponse<IEnumerable<Funerarias>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<Funerarias>("SGEV_FUNERARIAS_LISTAR", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<RegistroParametro>> Parametros()
        {
            var response = new BaseResponse<RegistroParametro>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryFirstOrDefaultAsync<RegistroParametro>("SGE_REGISTRO_PARAMETRO_LISTAR", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }
    }
}
