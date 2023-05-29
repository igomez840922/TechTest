using Microsoft.EntityFrameworkCore;
using Test.Shared.Models;

namespace Test.Shared.Contexts
{
    public class ProductRegistryContext : DbContext
    {
        public ProductRegistryContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Products);

            modelBuilder.Entity<Product>()
                .HasKey(nameof(Models.Product.UserID), nameof(Models.Product.ID));

            modelBuilder.Entity<Models.Type>()
                .HasData(
                    new Models.Type()
                    {
                        ID = 8844,
                        Description = "Admin"
                    },
                    new Models.Type()
                    {
                        ID = 4422,
                        Description = "User"
                    }
                );
        }

        public DbSet<User> User { get; set; }
        public DbSet<Models.Type> Type { get; set; }
        public DbSet<Product> Product { get; set; }

    }
}
