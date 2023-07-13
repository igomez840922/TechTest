using Newtonsoft.Json;
using System.Net.Http.Json;
using Test.Shared.Dtos;

namespace Test.Client.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<dynamic> Login(Login login)
        {
            var token = new TokenData();
            try
            {
                var content = JsonContent.Create(login);
                var response = await _httpClient.PostAsync("api/Acceso/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<TokenData>(jsonString);
                    if (token?.Token == null)
                    {
                        var resp = JsonConvert.DeserializeObject<ResponseData>(jsonString);
                        return resp;
                    }
                }
                return token;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al loguearse.", ex.Message);
            }
        }

        public async Task<ResponseData> NuevoUsuario(NuevoUsuario usuario)
        {
            try
            {
                var respuesta = new ResponseData();
                var content = JsonContent.Create(usuario);
                var response = await _httpClient.PostAsync($"api/Acceso/nuevoUsuario", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<ResponseData>(jsonString);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al loguearse.", ex.Message);
            }
        }
    }
}
