using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Server.Data;
using ProductosApp.Shared.Models;

namespace ProductosApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly DataContext _context;

		public ProductController(DataContext context)
		{
			_context = context;
		}

		[HttpGet] 
		public async Task<List<Product>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Product?> GetProductById(int id)
		{
			var dbProduct = await _context.Products.FindAsync(id);
			return dbProduct;
		}

		[HttpPost]
		public async Task<Product?> CreateProduct(Product product)
		{
			_context.Add(product);
			await _context.SaveChangesAsync();
			return product;
		}

		[HttpPut("{id}")]
		public async Task<Product?> UpdateProduct(int id, Product product)
		{
			{
				var dbProduct = await _context.Products.FindAsync(id);
				if (dbProduct != null)
				{
					dbProduct.ProductName = product.ProductName;
					dbProduct.ProductDescription = product.ProductDescription;
					dbProduct.ProductPrice = product.ProductPrice;
					dbProduct.ProductPhoto = product.ProductPhoto;

					await _context.SaveChangesAsync();
				}

				return dbProduct;
			}
		}

		[HttpDelete("{id}")]
		public async Task<bool> DeleteProduct(int id)
		{
			var dbProduct = await _context.Products.FindAsync(id);
			if (dbProduct == null)
			{
				return false;
			}

			_context.Remove(dbProduct);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}