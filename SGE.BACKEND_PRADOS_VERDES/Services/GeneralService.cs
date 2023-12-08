using Dapper;
using SGE.BACKEND_PRADOS_VERDES.Context;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;
using System.Data;

namespace SGE.BACKEND_PRADOS_VERDES.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly Conexion _conexion;

        public GeneralService(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<BaseResponse<IEnumerable<TablaRegistro>>> ListarTablaRegistroDetalle(int tablc_iid_tipo_tabla)
        {
            var response = new BaseResponse<IEnumerable<TablaRegistro>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@tablc_iid_tipo_tabla", tablc_iid_tipo_tabla);
                    response.Data = await conexion.QueryAsync<TablaRegistro>("USP_TABLA_REGISTRO_DETALLE_LISTAR", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<TablaVentasDetalle>>> ListarTablaVentasDetalle(int tabvc_iid_tipo_tabla)
        {
            var response = new BaseResponse<IEnumerable<TablaVentasDetalle>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@tabvc_iid_tipo_tabla", tabvc_iid_tipo_tabla);
                    response.Data = await conexion.QueryAsync<TablaVentasDetalle>("USP_TABLA_VENTAS_DET_LISTAR", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Vendedor>>> ListarVendedores()
        {
            var response = new BaseResponse<IEnumerable<Vendedor>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<Vendedor>("SGE_VENDEDOR_LISTAR", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<TablaVentasDetalle>>> TipoSepulturaByPlan(int tipoPlan, int nombrePlan)
        {
            var response = new BaseResponse<IEnumerable<TablaVentasDetalle>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@icod_tipo_plan", tipoPlan);
                    parametros.Add("@icod_nombre_plan", nombrePlan);
                    response.Data = await conexion.QueryAsync<TablaVentasDetalle>("USP_TIPO_SEPULTURA_BY_PLAN", parametros, commandType: CommandType.StoredProcedure);
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
