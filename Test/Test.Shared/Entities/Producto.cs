using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
