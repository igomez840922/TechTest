using Soaint.Test.Blazor.Shared.Entities;

namespace Soaint.Test.Blazor.Client.Services;

public interface IProductService
{
    List<Product> Products { get; set; }
    List<Model> Models { get; set; }

    Task GetModels();
    Task<Product> Get(int id);
    Task GetAll();
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int id);

}