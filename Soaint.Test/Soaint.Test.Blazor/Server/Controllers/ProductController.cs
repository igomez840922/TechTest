using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soaint.Test.Blazor.Server.Contexts;
using Soaint.Test.Blazor.Shared.Entities;

namespace Soaint.Test.Blazor.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
        => _context = context;

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _context.Products
            .Include(s => s.Model).ToListAsync();
        
        return Ok(products);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Model>>> GetAllModels()
    {
        var models = await _context.Models.ToListAsync();
        return Ok(models);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _context.Products
            .Include(s => s.Model)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (product is null)
            return NotFound("product not found.");
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<List<Product>>> Create(Product product)
    {
        product.Model = null;
        _context.Products.Add(product); 
        await _context.SaveChangesAsync();

        return Ok(await GetDbProducts());
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<List<Product>>> Update(Product product, int id)
    {
        var dbProduct = await _context.Products
            .Include(s => s.Model)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (dbProduct is null)
            return NotFound("product not found.");

        dbProduct.Name = product.Name;
        dbProduct.Description = product.Description;
        dbProduct.ModelId = product.ModelId;
        dbProduct.UnitPrice = product.UnitPrice;
        
        await _context.SaveChangesAsync();
        
        return Ok(await GetDbProducts());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<List<Product>>> Delete(int id)
    {
        var dbProduct = await _context.Products
            .Include(s => s.Model)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (dbProduct is null)
            return NotFound("product not found.");

        _context.Products.Remove(dbProduct);
        
        await _context.SaveChangesAsync();
        
        return Ok(await GetDbProducts());
    }

    private async Task<List<Product>> GetDbProducts()
        => await _context.Products.Include(s => s.Model).ToListAsync();
}