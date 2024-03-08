using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using Test.Client.Interfaces;
using Test.Shared.Entities;
using Test.Shared.Entities.DTO;

namespace Test.Client.Services
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ProductServices(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task<AppResult> AddProduct(ProductRequest product)
        {
            AppResult result = new AppResult();
            if (product is null)
            {
                result.Result = AppResultStatus.Failed;
                result.Message = "Product is null";
                return result;
            }
            try
            {
                var response = await _http.PostAsJsonAsync("api/Products", product);
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

        public async Task<AppResult?> DeleteProduct(string id)
        {
            AppResult deltedResult = new();
            var existingProduct = GetProductById(id);

            if (existingProduct is null)
            {
                deltedResult.Result = AppResultStatus.InternalError;
                deltedResult.Message = "product is not fount";
                return deltedResult;
            }

            var response = await _http.DeleteAsync($"api/Products/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/logout");
                return null;
            }

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

        public async Task<ProductResponse?> GetProductById(string id)
        {
            var response = await _http.GetAsync($"api/Products/{id}");
            string content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/logout");
                return null;
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var product = JsonSerializer.Deserialize<ProductResponse>(content, options);
                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AppResult> UpdateProduct(string id, ProductRequest product)
        {
            AppResult updatedResult = new();
            var existingProduct = await GetProductById(id ?? string.Empty);

            if (existingProduct is null)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = "product is not fount";
                return updatedResult;
            }

            var response = await _http.PutAsJsonAsync($"api/Products/{id}", product);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = "unauthorized";
                _navigationManager.NavigateTo("/logout");
                return updatedResult;
            }
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

        public async Task<List<ProductResponse>> GetAllProduct()
        {
            var list = new List<ProductResponse>();

            try
            {
                if (!_http.DefaultRequestHeaders.Authorization?.Parameter?.Contains("ey") ?? false)
                {
                    await Task.Delay(1000);
                }

                var response = await _http.GetAsync($"api/Products");
                string content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/logout");
                    return list;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var obj = JsonSerializer.Deserialize<IEnumerable<ProductResponse>>(content, options);
                list = obj?.ToList() ?? new List<ProductResponse>();
            }
            catch (Exception)
            {

            }

            return list;
        }
    }
}
