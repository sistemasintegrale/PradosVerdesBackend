using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Interfaces
{
    public interface IContratoService
    {
        Task<BaseResponse<IEnumerable<Contrato>>> ContratoListarPorFechas(ContratoFiltersDto filter);
        Task<BaseResponse<Contrato>> ContratoGetById(int cntc_icod_contrato );
        Task<BaseResponse<int>> ContratoInsertar(Contrato contrato);
        Task<BaseResponse<bool>> ContratoModificar(Contrato contrato);
        Task<BaseResponse<bool>> ContratoIEliminar(int cntc_icod_contrato);

        Task<BaseResponse<IEnumerable<Funerarias>>> Funerarias();
        Task<BaseResponse<IEnumerable<Distrito>>> Distritos();
        Task<BaseResponse<RegistroParametro>> Parametros();
    }
}
