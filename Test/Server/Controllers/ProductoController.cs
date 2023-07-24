using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Test.Server.AuxService;
using Test.Shared.Entities;

namespace Test.Server.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ISourceStorage storage;
        private readonly IMapper mapper;
        private readonly string contenedor = "ProdFotos";

        public ProductoController(AppDbContext context, ISourceStorage storage, IMapper mapper)
        {
            this.context = context;
            this.storage = storage;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await context.Producto.ToListAsync();
        }

        [HttpGet("{productoId:int}")]
        public async Task<ActionResult<Producto>> Get(int productoId)
        {
            var producto = await context.Producto.FirstOrDefaultAsync(producto => producto.ProductoId == productoId);

            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Producto producto)
        {
            if (!string.IsNullOrWhiteSpace(producto.Foto))
            {

                var fotoActor = Convert.FromBase64String(producto.Foto);
                producto.Foto = await storage.GuardarArchivo(fotoActor, ".jpg", contenedor);
            }

            context.Add(producto);
            await context.SaveChangesAsync();
            return producto.ProductoId;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Producto producto)
        {
            var product = await context.Producto.FirstOrDefaultAsync(p => p.ProductoId == producto.ProductoId);
            if (product is null)
            {
                return NotFound();
            }

            product = mapper.Map(producto, product);
            if (!string.IsNullOrWhiteSpace(producto.Foto))
            {
                var fotoP = Convert.FromBase64String(producto.Foto);
                product.Foto = await storage.EditarArchivo(fotoP, ".jpg", contenedor, producto.Foto!);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{productoId:int}")]
        public async Task<ActionResult> Delete(int productoId)
        {
            var productoEliminado = await context.Producto.FirstOrDefaultAsync(p => p.ProductoId == productoId);

            if (productoEliminado == null)
            {
                return NotFound();
            }

            context.Remove(productoEliminado);
            await context.SaveChangesAsync();
            await storage.EliminarArchivo(productoEliminado.Foto!, contenedor);
            return NoContent();
        }


    }
}
