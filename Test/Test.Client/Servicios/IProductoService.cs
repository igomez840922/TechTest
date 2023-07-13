using Test.Shared.Dtos;
using Test.Shared.Entities;

namespace Test.Client.Servicios
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos(string token);
        Task<Producto> ObtenerProductoPorId(int IdProducto, string token);
        Task<ResponseData> NuevoProducto(NuevoProducto producto, string token);
        Task<ResponseData> ActualizarProducto(Producto producto, string token);
        Task<ResponseData> EliminarProducto(int Idproducto, string token);
    }
}
