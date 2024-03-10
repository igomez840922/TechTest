using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Soaint.Test.Blazor.Shared.Entities;

namespace Soaint.Test.Blazor.Server.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Model>().HasData(
            new Model { Description = "Volvo", Id = 1 },
            new Model { Description = "Ford", Id = 2 });
        builder.Entity<Product>();
        
        base.OnModelCreating(builder);
    }

    public DbSet<Model> Models { get; set; }
    public DbSet<Product> Products { get; set; }
}