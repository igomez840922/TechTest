using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Server.Repositories;
using Test.Shared.DTOS;

namespace Test.Server.Controllers.Product
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("products")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsAsync(GetUserProductsDTO getUserProductsDTO)
        {
            List<ProductDTO> products = new List<ProductDTO>();

            try
            {
                products = await _productRepository.GetAllProductsAsync(getUserProductsDTO);
            }
            catch (Exception ex)
            {

                
            }

            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ProductDTO>> AddProductAsync(AddProductDTO addProductDTO)
        {
            ProductDTO product = new ProductDTO();

            try
            {
                product = await _productRepository.AddProductAsync(addProductDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status201Created, product);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ProductDTO>> EditProductAsync(ProductDTO product)
        {
            ProductDTO editedProduct = new ProductDTO();

            try
            {
                editedProduct = await _productRepository.EditProductAsync(product);

                if(editedProduct == null || editedProduct.ID == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, product);
                }
            }
            catch (Exception ex)
            {
                
            }

            return StatusCode(StatusCodes.Status200OK, editedProduct);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteProductAsync(ProductDTO product)
        {
            try
            {
                bool deleted = await _productRepository.DeleteProductAsync(product);

                if (!deleted)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
