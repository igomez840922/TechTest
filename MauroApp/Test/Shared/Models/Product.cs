using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductosApp.Shared.Models
{
	public class Product
	{
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Producto es requerido")]
        public string? ProductName { get; set; }

		public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "El campo Precio de Producto es requerido")]
        public int ProductPrice { get; set; }

        [Required(ErrorMessage = "El campo Foto de Producto es requerido")]
        public string? ProductPhoto { get; set; }
	}
}
