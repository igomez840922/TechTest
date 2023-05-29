using Test.Shared.DTOS;

namespace Test.Server.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDTO> AddProductAsync(AddProductDTO addProductDTO);
        Task<List<ProductDTO>> GetAllProductsAsync(GetUserProductsDTO getUserProductsDTO);
        Task<ProductDTO> EditProductAsync(ProductDTO editProductDTO);
    }
}
