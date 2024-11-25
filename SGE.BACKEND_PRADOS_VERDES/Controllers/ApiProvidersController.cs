using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SGE.BACKEND_PRADOS_VERDES.Controllers
{
    [Route("api/ApiProviders")]
    [ApiController]
     
    public class ApiProvidersController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{dni}")]
        public async Task<ActionResult> GetUserData([FromRoute] string dni) {
            // URL de la API
            string apiUrl = $"https://api.apis.net.pe/v2/reniec/dni?numero={dni}";

            // Token de autenticación
            string apiToken = "apis-token-6682.MkbxSNpN4bgdckEbQlg0V506-Lg3er4P";

            string response = string.Empty; 

            // Crear una instancia de HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Agregar el token de autenticación en los headers
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");

                try
                {
                    // Realizar la solicitud HTTP GET a la API
                    HttpResponseMessage res = await client.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa (código de estado 200 OK)
                    if (res.IsSuccessStatusCode)
                    {
                        // Leer y mostrar el contenido de la respuesta
                        response = await res.Content.ReadAsStringAsync();
                       
                    }
                  
                   
                }
                catch (Exception ex)
                {
                     
                }
            }
            return Ok(response);
        }
    }
}
