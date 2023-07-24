using System.Text.Json;
using System.Text;

namespace Test.Client.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HttpClient http;

        public ServiceRepository(HttpClient http)
        {
            this.http = http;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var respuestaHTTP = await http.GetAsync(url);

            if (respuestaHTTP.IsSuccessStatusCode)
            {
                var respuesta = await DeserializarRespuesta<T>(respuestaHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(respuesta, error: false, respuestaHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, error: true, respuestaHTTP);
            }
        }


        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await http.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await http.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await http.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null,
                !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await http.PostAsync(url, enviarContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp,
                    OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, error: false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default,
                !responseHttp.IsSuccessStatusCode, responseHttp);
        }
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse,
      JsonSerializerOptions jsonSerializerOptions)
        {
            var respuestaString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions)!;
        }
    }
}
