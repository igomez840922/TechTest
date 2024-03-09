using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Test.Server.Models;
using Test.Shared.Entities.DataBase;

namespace Test.Server.Data
{
    public class TestServerContext : IdentityDbContext<ApplicationUser>
    {
        public TestServerContext(
            DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
            modelBuilder.Entity<Product>().Property("Id").HasDefaultValueSql("Newid()");

        }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Test.Shared.Entities.DataBase.Product>? Product { get; set; }
    }
}
