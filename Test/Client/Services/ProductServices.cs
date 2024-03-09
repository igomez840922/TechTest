using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using Test.Client.Interfaces;
using Test.Client.Pages;
using Test.Shared.Entities;
using Test.Shared.Entities.DataBase;
using ClosedXML.Excel;

namespace Test.Client.Services
{
    public class ProductServices : IProductServices
    {
        public ProductServices(HttpClient http, NavigationManager navigationManager)
        {
            Http = http;
            NavigationManager = navigationManager;
        }
        public HttpClient Http { get; }
        public NavigationManager NavigationManager { get; }

        public async Task<AppResult> AddProduct(Product product)
        {
            AppResult result = new AppResult();
            if (product == null)
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
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                NavigationManager.NavigateTo("/logout");
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
        public async Task<Product> GetProductById(string id)
        {
            var response = await Http.GetAsync($"api/Products/{id}");
            string content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                NavigationManager.NavigateTo("/logout");
                return null;
            }
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var product = JsonSerializer.Deserialize<Product>(content, options);
                return product;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<AppResult> UpdateProduct(Product product)
        {
            AppResult updatedResult = new AppResult();
            var existingProduct = await GetProductById(product?.Id);
            if (existingProduct == null)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = "product is not fount";
                return updatedResult;
            }

            var response = await Http.PutAsJsonAsync($"api/Products/{product.Id}", product);
            string content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                updatedResult.Result = AppResultStatus.InternalError;
                updatedResult.Message = "unauthorized";
                NavigationManager.NavigateTo("/logout");
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
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    NavigationManager.NavigateTo("/logout");
                    return list;
                }
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
            catch (Exception ex)
            {
                var cv = ex;
            }

            return list;
        }

        public async Task<byte[]> ExportAllToExcel()
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
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    NavigationManager.NavigateTo("/logout");
                }
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
            catch (Exception ex)
            {
                var cv = ex;
            }

            // Create a new workbook
            using (var workbook = new XLWorkbook())
            {
                // Create a new worksheet
                var worksheet = workbook.Worksheets.Add("Products");

                // Write the header row
                worksheet.Cell(1, 1).Value = "Product Id";
                worksheet.Cell(1, 2).Value = "Product Name";
                worksheet.Cell(1, 3).Value = "Product Description";
                worksheet.Cell(1, 4).Value = "Product Price";

                // Write the data rows
                int row = 2;
                foreach (Product product in list)
                {
                    worksheet.Cell(row, 1).Value = product.Id;
                    worksheet.Cell(row, 2).Value = product.Name;
                    worksheet.Cell(row, 3).Value = product.Description;
                    worksheet.Cell(row, 4).Value = product.Price;
                    row++;
                }

                // Save the workbook to a memory stream
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public Task<byte[]> ExportAllToPdf()
        {
            throw new NotImplementedException();
        }
    }
}
