using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Test.Shared.Dtos;
using Test.Shared.Entities;

namespace Test.Client.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData> ActualizarProducto(Producto producto, string token)
        {
            try
            {
                var respuesta = new ResponseData();
                var content = JsonContent.Create(producto);
                var response = await _httpClient.PutAsync($"api/Producto/actualizarProducto", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<ResponseData>(jsonString);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al actualizar el producto.", ex.Message);
            }
        }

        public async Task<ResponseData> EliminarProducto(int Idproducto, string token)
        {
            try
            {
                var respuesta = new ResponseData();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await _httpClient.DeleteAsync($"api/Producto/eliminarProductoPorId/{Idproducto}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<ResponseData>(jsonString);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al eliminar el producto.", ex.Message);
            }
        }

        public async Task<ResponseData> NuevoProducto(NuevoProducto producto, string token)
        {
            try
            {
                var respuesta = new ResponseData();
                var content = JsonContent.Create(producto);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await _httpClient.PostAsync($"api/Producto/nuevoProducto", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<ResponseData>(jsonString);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al registrar el producto.", ex.Message);
            }
        }

        public async Task<Producto> ObtenerProductoPorId(int IdProducto, string token)
        {
            try
            {
                var producto = new Producto();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await _httpClient.GetAsync($"api/Producto/obtenerProductoPorId/{IdProducto}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    producto = JsonConvert.DeserializeObject<Producto>(jsonString);
                }

                return producto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al obtener el producto por su id.", ex.Message);
            }
        }

        public async Task<List<Producto>> ObtenerProductos(string token)
        {
            var lista = new List<Producto>();
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    var response = await _httpClient.GetAsync("api/Producto/obtenerProductos");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        lista = JsonConvert.DeserializeObject<List<Producto>>(jsonString);
                    }
                }
                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presento un error al obtener los productos.", ex.Message);
            }
        }
    }
}
