﻿using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Interfaces
{
    public interface IGeneralService
    {
        Task<BaseResponse<IEnumerable<TablaRegistro>>> ListarTablaRegistroDetalle(int icod);
        Task<BaseResponse<IEnumerable<TipoSepulturaDto>>> TipoSepulturaByPlan(int tipoPlan, int nombrePlan, int tipoSepultura);
        Task<BaseResponse<IEnumerable<TipoSepulturaDetalle>>> PlanNesecidadSepulturaDetalle(int id_cab);
        Task<BaseResponse<IEnumerable<TablaVentasDetalle>>> ListarTablaVentasDetalle(int icod);
        Task<BaseResponse<IEnumerable<Vendedor>>> ListarVendedores();
    }
}
