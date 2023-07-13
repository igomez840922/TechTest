using Microsoft.EntityFrameworkCore;
using Test.Shared.Entities;

namespace Test.Data.ContextSoaint
{
    public class SoaintContext : DbContext
    {
        public SoaintContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Acceso> Acceso { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
    }
}
