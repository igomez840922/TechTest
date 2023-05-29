using Microsoft.EntityFrameworkCore;
using Test.Shared.Contexts;
using Test.Shared.DTOS;
using Test.Shared.Extensions;
using Test.Shared.Models;

namespace Test.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductRegistryContext _context;
        public ProductRepository(ProductRegistryContext context)
        {
            _context = context;
        }

        public async Task<ProductDTO> AddProductAsync(AddProductDTO addProductDTO)
        {
            List<Product> products = _context.Product.Where(p => p.UserID == addProductDTO.UserID).ToList();

            long max = 1;

            if(products.Count > 0)
            {
                max = products.Max(p => p.ID) + 1;
            }

            Product newProduct = new Product();

            newProduct.Name = addProductDTO.Name;
            newProduct.Description = addProductDTO.Description;
            newProduct.Model = addProductDTO.Model;
            newProduct.Price = addProductDTO.Price;
            newProduct.UserID = addProductDTO.UserID;
            newProduct.ImageData = addProductDTO.ImageData;
            newProduct.MimeType = addProductDTO.MimeType;
            newProduct.ID = max;

            await _context.Product.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.ProductAsDTO();
        }

        public async Task<bool> DeleteProductAsync(ProductDTO deleteProductDTO)
        {
            Product? existingProduct = _context.Product.Where(p => p.UserID == deleteProductDTO.UserID && p.ID == deleteProductDTO.ID).FirstOrDefault();

            if(existingProduct == null)
            {
                return false;
            }

            await Task.FromResult(_context.Product.Remove(existingProduct));
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDTO> EditProductAsync(ProductDTO product)
        {
            Product? existingProduct = await _context.Product.Where(p => p.UserID == product.UserID && p.ID == product.ID).FirstOrDefaultAsync();

            if(existingProduct == null)
            {
                return new ProductDTO();
            }

            existingProduct.Name = product.Name;
            existingProduct.Model = product.Model;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.ImageData = product.ImageData;
            existingProduct.MimeType = product.MimeType;

            _context.Entry<Product>(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingProduct.ProductAsDTO();
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(GetUserProductsDTO getUserProductsDTO)
        {
            List<ProductDTO> products = new List<ProductDTO>();

            products = await _context.Product.Where(p => p.UserID == getUserProductsDTO.UserID).Select(p => p.ProductAsDTO()).ToListAsync();

            return products;
        }
    }
}
