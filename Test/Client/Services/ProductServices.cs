using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using Test.Client.Interfaces;
using Test.Client.Pages;
using Test.Shared.Entities;
using Test.Shared.Entities.DataBase;
namespace Test.Client.Services
{
    public class ProductServices : IProductServices
    {
        public ProductServices(HttpClient http)
        {
            Http = http;
        }
        public HttpClient Http { get; }


        public async Task<AppResult> AddProduct(Product product)
        {
            AppResult result = new AppResult();
            if(product == null)
            {
                result.Result = AppResultStatus.Failed;
                result.Message = "Product is null";
                return result;
            }    
            try
            {
                product.Id = Guid.NewGuid().ToString();
                var response = await Http.PostAsJsonAsync("api/Products", product);
                string content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result.Result = AppResultStatus.Ok;
                    result.Message = "created succcessfully";
                }
            }
            catch (Exception ex)
            {
                result.Result = AppResultStatus.InternalError;
                result.Message = $"the product could not be created {ex}";
            }
            return result;
        }

        public async Task<AppResult> DeleteProduct(string id)
        {
            AppResult deltedResult = new AppResult();
            var existingProduct = GetProductById(id);
            if (existingProduct == null)
            {
                deltedResult.Result = AppResultStatus.InternalError;
                deltedResult.Message = "product is not fount";
                return deltedResult;
            }
            var response = await Http.DeleteAsync($"api/Products/{id}");
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("product can deleted");
                    deltedResult.Result = AppResultStatus.Ok;
                    deltedResult.Message = "delete successfully";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("product can deleted");
                deltedResult.Result = AppResultStatus.InternalError;
                deltedResult.Message = $"delete is not Succesful {ex}";
            }
            return deltedResult;

        }
        public async Task<Product> GetProductById(string id)
        {
            var response = await Http.GetAsync($"api/Products/{id}");
            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var product = JsonSerializer.Deserialize<Product>(content, options);
            return product;
        }
        public async Task<AppResult> UpdateProduct(Product product)
        {
            AppResult updatedResult = new AppResult();
            var existingProduct = await GetProductById(product.Id);
            if (existingProduct == null)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = "product is not fount";
                return updatedResult;
            }
            var productNew = new Product()
            {
                Id = product.Id,
                Description = product.Description,
                Model = product.Model,
                Name = product.Name,
                Photo = product.Photo,
                Price = product.Price,
            };

            var response = await Http.PutAsJsonAsync($"api/Products/{productNew.Id}", productNew);
            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    updatedResult.Result = AppResultStatus.Ok;
                    updatedResult.Message = "created succcessfully";
                }
            }
            catch (Exception ex)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = $"the product could not be created {ex}";
            }
            return updatedResult;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            var list = new List<Product>();
            try
            {
                if (!Http.DefaultRequestHeaders.Authorization.Parameter.Contains("ey") || Http.DefaultRequestHeaders.Authorization == null)
                {
                    await Task.Delay(1000);
                }
                var response = await Http.GetAsync($"api/Products");
                string content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var obj = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                foreach (var item in obj)
                {
                    list.Add(item);
                }
            }
            catch(Exception ex)
            {

            }
            
            return list;
        }
    }
}
