
using Microsoft.AspNetCore.Http;

namespace Test.Shared.Dtos
{
    public class NuevoProducto
    {
        public int IdUsuario { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public IFormFile ImgProducto { get; set; }
    }
}
