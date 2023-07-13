using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Test.ServiceDependencies.Context;
using Test.Shared.Dtos;
using Test.Shared.Entities;

namespace Test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoContext _productoContext;
        private readonly IMapper _mapper;

        public ProductoController(IProductoContext productoContext, IMapper mapper)
        {
            _productoContext = productoContext;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        [Route("obtenerProductos")]
        public async Task<IActionResult> obtenerProductos()
        {
            try
            {
                var productos = await _productoContext.ObtenerProductos();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al obtener los productos.", ex.Message);
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("obtenerProductoPorId")]
        public async Task<IActionResult> obtenerProductoPorId(int IdProducto)
        {
            try
            {
                var producto = await _productoContext.ObtenerProductoPorId(IdProducto);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al obtener un producto por su id.", ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("nuevoProducto")]
        public async Task<IActionResult> nuevoProducto([FromForm] NuevoProducto producto)
        {
            try
            {
                var product = _mapper.Map<Producto>(producto);
                var response = await _productoContext.NuevoProducto(product, producto.ImgProducto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al agregar el nuevo producto.", ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("actualizarProducto")]
        public async Task<IActionResult> actualizarProducto(ActualizarProducto producto)
        {
            try
            {
                var product = _mapper.Map<Producto>(producto);
                var response = await _productoContext.ActualizarProducto(product);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al actualizar el producto.", ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("eliminarProductoPorId")]
        public async Task<IActionResult> eliminarProductoPorId(int IdProducto)
        {
            try
            {
                var response = await _productoContext.EliminarProducto(IdProducto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al eliminar el producto.", ex.Message);
            }
        }
    }
}
