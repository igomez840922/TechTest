using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Soaint.Test.Blazor.Shared.Entities;

namespace Soaint.Test.Blazor.Client.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _client;
    private readonly NavigationManager _navigationManager;

    public ProductService(HttpClient client, NavigationManager navigationManager)
    {
        _client = client;
        _navigationManager = navigationManager;
    }

    public List<Product> Products { get; set; } = new();
    public List<Model> Models { get; set; } = new();

    public async Task GetModels()
    {
        var result = await _client.GetFromJsonAsync<List<Model>>("api/Product/GetAllModels");
        if (result is not null)
            Models = result;
    }

    public async Task<Product> Get(int id)
    {
        var result = await _client.GetFromJsonAsync<Product>($"api/Product/Get/{id}");
        if (result is not null)
            return result;

        throw new Exception("Product not found.");
    }

    public async Task GetAll()
    {
        var result = await _client.GetFromJsonAsync<List<Product>>("api/Product/GetAll");
        if (result is not null)
            Products = result;
    }

    public async Task Create(Product product)
    {
        var result = await _client.PostAsJsonAsync("api/Product/Create", product);
        await SetProducts(result);
    }

    public async Task Update(Product product)
    {
        var result = await _client.PutAsJsonAsync($"api/Product/Update/{product.Id}", product);
        await SetProducts(result);
    }

    public async Task Delete(int id)
    {
        var result = await _client.DeleteAsync($"api/Product/Delete/{id}");
        await SetProducts(result);
    }

    private async Task SetProducts(HttpResponseMessage result)
    {
        var response = await result.Content.ReadFromJsonAsync<List<Product>>();
        Products = response;
        _navigationManager.NavigateTo("products");
    }
}