using System;
using System.Data;
using AutoMapper;
using Dapper;
using SGE.BACKEND_PRADOS_VERDES.Context;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Services
{
    public class CuotaService : ICuotaService
    {
        private readonly Conexion _conexion;
        public CuotaService(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<BaseResponse<bool>> CuotaEliminar(Cuota cuota)
        {
            var response = new BaseResponse<bool>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@cntc_icod_contrato_cuotas", cuota.cntc_icod_contrato_cuotas);
                    parametros.Add("@cntc_iusuario_modifica", cuota.cntc_iusuario_crea);
                    parametros.Add("@cntc_vpc_modifica", cuota.cntc_vpc_crea);
                    await conexion.ExecuteAsync("SGEV_CONTRATOS_CUOTAS_ELIMINAR", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> CuotaGuardar(Cuota cuota)
        {
            var response = new BaseResponse<bool>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    await conexion.ExecuteAsync("USP_CUOTA_GUARDAR", cuota, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Cuota>>> CuotaListar(int cntc_icod_contrato)
        {
            var response = new BaseResponse<IEnumerable<Cuota>>();
            try
            {

                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@cntc_icod_contrato", cntc_icod_contrato);
                    response.Data = await conexion.QueryAsync<Cuota>("USP_CONTRATOS_CUOTAS_LISTAR", parametros, commandType: CommandType.StoredProcedure);
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

