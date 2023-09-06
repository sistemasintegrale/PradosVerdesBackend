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

        public Task<BaseResponse<bool>> ContratoIEliminar(int cntc_icod_contrato)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<int>> ContratoInsertar(Contrato contrato)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Contrato>>> ContratoListarPorFechas(ContratoFiltersDto filter)
        {
            var response = new BaseResponse<IEnumerable<Contrato>>();
            try
            {
                var param = _mapper.Map<ContratoFilter>(filter);
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<Contrato>("USP_CONTRATO_LISTAR_POR_FECHA", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public Task<BaseResponse<bool>> ContratoModificar(Contrato contrato)
        {
            throw new NotImplementedException();
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
