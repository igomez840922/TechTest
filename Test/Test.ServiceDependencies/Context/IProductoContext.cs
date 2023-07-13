using Microsoft.AspNetCore.Http;
using Test.Shared.Dtos;
using Test.Shared.Entities;

namespace Test.ServiceDependencies.Context
{
    public interface IProductoContext
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoPorId(int IdProducto);
        Task<ResponseData> NuevoProducto(Producto producto, IFormFile fotoProducto);
        Task<ResponseData> ActualizarProducto(Producto producto);
        Task<ResponseData> EliminarProducto(int Idproducto);
    }
}
