using Test.Shared.Entities;
using Test.Shared.Entities.DTO;

namespace Test.Client.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductResponse>> GetAllProduct();
        Task<ProductResponse?> GetProductById(string id);
        Task <AppResult?> DeleteProduct(string id);
         Task <AppResult> AddProduct(ProductRequest product);
        Task <AppResult> UpdateProduct(string id, ProductRequest product);                   
    }
}
