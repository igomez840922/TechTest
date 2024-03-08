using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Server.Data;
using Test.Shared.Entities.DataBase;
using Test.Shared.Entities.DTO;

namespace Test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly TestServerContext _context;

        public ProductsController(TestServerContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            try
            {
                var products = await _context.Product.ToListAsync();
                var producList = products.Adapt<IEnumerable<ProductResponse>>();

                return Ok(producList);
            }
            catch
            {
                throw;
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(string id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product is null)
                {
                    return NotFound();
                }

                return product.Adapt<ProductResponse>();
            }
            catch
            {
                throw;
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, ProductRequest productRequest)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);

                if (product is null)
                {
                    return NotFound();
                }

                product.Description = productRequest.Description;
                product.Photo = productRequest.Photo;
                product.Name = productRequest.Name;
                product.Price = productRequest.Price;
                product.Model = productRequest.Model;

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> PostProduct(ProductRequest productRequest)
        {
            try
            {
                var product = productRequest.Adapt<Product>();
                _context.Product.Add(product);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    var productResponse = product.Adapt<ProductResponse>();
                    return Ok(productResponse);
                }
                else
                {
                    return StatusCode(500, "table could not be created");
                }
            }
            catch
            {
                throw;
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product is null)
                {
                    return NotFound();
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
