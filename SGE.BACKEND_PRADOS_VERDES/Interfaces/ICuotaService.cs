using System;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Interfaces
{
    public interface ICuotaService
    {
        Task<BaseResponse<IEnumerable<Cuota>>> CuotaListar(int cntc_icod_contrato);
        Task<BaseResponse<bool>> CuotaEliminar(Cuota cuota);
        Task<BaseResponse<bool>> CuotaGuardar(Cuota cuota);
    }
}

