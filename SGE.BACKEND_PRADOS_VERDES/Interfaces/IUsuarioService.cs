using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Models;

namespace SGE.BACKEND_PRADOS_VERDES.Interfaces
{
    public interface IUsuarioService
    {
        Task<BaseResponse<IEnumerable<Usuario>>> UsuarioList();
        Task<BaseResponse<Usuario>> UsuarioGetById(int icod);
        Task<BaseResponse<string>> GenerateToken(TokenRequestDto tokenRequestDto);
    }
}
