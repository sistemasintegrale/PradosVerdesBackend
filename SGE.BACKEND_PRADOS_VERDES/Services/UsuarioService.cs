using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SGE.BACKEND_PRADOS_VERDES.Common;
using SGE.BACKEND_PRADOS_VERDES.Context;
using SGE.BACKEND_PRADOS_VERDES.Dtos;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGE.BACKEND_PRADOS_VERDES.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Conexion _conexion;
        private IConfiguration _configuration;

        public UsuarioService(Conexion conexion, IConfiguration configuration)
        {
            _conexion = conexion;
            _configuration = configuration;
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto tokenRequestDto)
        {
            var response = new BaseResponse<string>();
            var users = await UsuarioList();
            var account = users.Data!.Where(x => x.usua_codigo_usuario == tokenRequestDto.email.ToUpper()).FirstOrDefault();
            if (account is not null)
            {
                if (Encriptador.decod(account.usua_password_usuario!) == tokenRequestDto.password)
                {
                    response.Data = GenerateToken(account);
                    response.Mensaje = ReplyMessage.MESSAGE_TOKEN;
                    return response;
                }
                else
                {
                    response.Mensaje = ReplyMessage.MESSAGE_TOKEN_ERROR;
                    response.IsSucces = false;
                }
            }
            else
            {
                response.IsSucces = false;
                response.Mensaje = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }
            return response;
        }
        public string GenerateToken(Usuario user)
        {
            var securittyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var credencials = new SigningCredentials(securittyKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId,user.usua_icod_usuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.usua_codigo_usuario!),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.usua_codigo_usuario !),
                new Claim(JwtRegisteredClaimNames.GivenName,user.usua_codigo_usuario !),
                new Claim(JwtRegisteredClaimNames.Jti,user.usua_icod_usuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString(),ClaimValueTypes.Integer64),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credencials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<Usuario>> UsuarioGetById(int usua_icod_usuario)
        {
            var response = new BaseResponse<Usuario>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("usua_icod_usuario", usua_icod_usuario);
                    response.Data = await conexion.QueryFirstAsync<Usuario>("USP_USUARIO_GET_BY_ID", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.innerExeption = ex.Message;
                response.IsSucces = false;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Usuario>>> UsuarioList()
        {
            var response = new BaseResponse<IEnumerable<Usuario>>();
            try
            {
                using (var conexion = _conexion.ObtenerConnexion())
                {
                    response.Data = await conexion.QueryAsync<Usuario>("USP_USUARIO_LISTAR", commandType: CommandType.StoredProcedure);
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
