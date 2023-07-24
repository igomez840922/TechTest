using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Shared.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }
        [Required]
        [StringLength(maximumLength: 250)]
        public string Nombre { get; set; } = null!;
        [Required]
        public int Modelo { get; set; }
        [StringLength(maximumLength: 1000)]
        public string? Descripcion { get; set; }
        [Required]
        public int precio { get; set; }

        [Required]
        [DisplayName("Upload image here")]
        public string Foto { get; set; } = null!;
    }
}
