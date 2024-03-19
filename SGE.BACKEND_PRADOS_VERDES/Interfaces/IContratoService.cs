using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Filters;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Interfaces
{
    public interface IContratoService
    {
        Task<BaseResponse<IEnumerable<ContratoDTO>>> ContratoListarPorFechas(ContratoFiltersDto filter);
        Task<BaseResponse<Contrato>> ContratoGetById(int cntc_icod_contrato);
        Task<BaseResponse<int>> ContratoGuardar(Contrato contrato, Fallecido fallecido, Contratante princial, Contratante secundario);
        Task<BaseResponse<IEnumerable<Funerarias>>> Funerarias();
        Task<BaseResponse<IEnumerable<Distrito>>> Distritos();
        Task<BaseResponse<RegistroParametro>> Parametros();
        Task<BaseResponse<int>> ContratoValidarSerie(string serie);
        Task<BaseResponse<bool>> ContratoEliminar(EliminarDTO dTO);
        Task<BaseResponse<ContratoImpresion>> ContratoImpresion(int cntc_icod_contrato);
    }
}
