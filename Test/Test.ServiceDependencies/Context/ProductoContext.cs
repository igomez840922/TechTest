using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Test.Data.ContextSoaint;
using Test.Shared.Dtos;
using Test.Shared.Entities;
using Test.Shared.Utilidades;

namespace Test.ServiceDependencies.Context
{
    public class ProductoContext : IProductoContext
    {
        private readonly SoaintContext _soaintContext;
        private readonly IMainSettings _mainSettings;

        public ProductoContext(SoaintContext soaintContext, IMainSettings mainSettings)
        {
            _soaintContext = soaintContext;
            _mainSettings = mainSettings;
        }

        public async Task<ResponseData> ActualizarProducto(Producto producto)
        {
            try
            {
                var prod = await _soaintContext.Producto.FindAsync(producto.IdProducto);
                if (prod == null)
                {
                    return new ResponseData()
                    {
                        Code = 401,
                        Mensaje = "El producto no existe"
                    };
                }

                prod.Nombre = producto.Nombre;
                prod.Descripcion = producto.Descripcion;
                prod.Precio = producto.Precio;
                prod.FechaActualizacion = DateTime.Now;

                await _soaintContext.SaveChangesAsync();

                return new ResponseData()
                {
                    Code = 200,
                    Mensaje = "Producto actualizado con exito."
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al modificar el producto.", ex.Message);
            }
        }

        public async Task<ResponseData> EliminarProducto(int Idproducto)
        {
            try
            {
                var producto = await _soaintContext.Producto.FindAsync(Idproducto);
                if (producto == null)
                {
                    return new ResponseData()
                    {
                        Code = 201,
                        Mensaje = "El producto no existe."
                    };
                }

                var path = producto.Foto.Replace(_mainSettings.PathFotoProducto, "").Split(@"\")[0];
                EliminarFotoProducto(_mainSettings.PathFotoProducto + path);


                _soaintContext.Remove(producto);
                await _soaintContext.SaveChangesAsync();

                return new ResponseData()
                {
                    Code = 200,
                    Mensaje = "Registro eliminado con exito"
                };

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al eliminar el producto.", ex.Message);
            }
        }

        private void EliminarFotoProducto(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al eliminar la foto del producto.", ex.Message);
            }
        }

        public async Task<ResponseData> NuevoProducto(Producto producto, IFormFile fotoProducto)
        {
            try
            {
                var folder = AppContext.BaseDirectory + (@"\Producto\") + Guid.NewGuid() + @"\";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var filePath = Path.Combine(folder, fotoProducto.FileName);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fotoProducto.CopyToAsync(fileStream);
                }

                producto.Foto = filePath;
                producto.FechaCreacion = DateTime.Now;
                var prod = await Task.FromResult(_soaintContext.Producto.Where(x => x.Nombre.Equals(producto.Nombre)).Any());
                if (prod)
                {
                    return new ResponseData()
                    {
                        Code = 201,
                        Mensaje = "El producto ya existe",
                        Error = string.Empty
                    };
                }
                await _soaintContext.Producto.AddAsync(producto);
                await _soaintContext.SaveChangesAsync();
                return new ResponseData()
                {
                    Code = 200,
                    Mensaje = "Producto agregado correctamente",
                    Error = string.Empty
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al agregar un nuevo producto.", ex.Message);
            }
        }

        public async Task<Producto> ObtenerProductoPorId(int IdProducto)
        {
            try
            {
                var producto = await _soaintContext.Producto.FindAsync(IdProducto);
                return producto;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al obtener un producto por su id.", ex.Message);
            }
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            try
            {
                var productos = await _soaintContext.Producto.ToListAsync();
                return productos;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al obtener los productos.", ex.Message);
            }
        }
    }
}
