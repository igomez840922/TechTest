using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Shared.DTOS
{
    public record AddProductDTO
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "La ID del usuario es requerida.")]
        public long UserID { get; set; }
        [MaxLength(255, ErrorMessage = "El modelo del producto no puede exceder los 255 caracteres.")]
        [MinLength(1, ErrorMessage = "El modelo del producto debe ser de al menos 1 carácter.")]
        [Required(ErrorMessage = "El modelo del producto es requerido.")]
        public string Model { get; set; }
        [MaxLength(255, ErrorMessage = "El nombre del producto no puede exceder los 255 caracteres.")]
        [MinLength(1, ErrorMessage = "El nombre del producto debe ser de al menos 1 carácter.")]
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string Name { get; set; }
        [MaxLength(1024, ErrorMessage = "La descripción del producto no puede exceder los 1024 caracteres.")]
        [MinLength(1, ErrorMessage = "La descripción del producto debe ser de al menos 1 carácter.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "El precio es requerido.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Debe subir una imagen del producto.")]
        public string ImageData { get; set; }
        [Required(ErrorMessage = "Debe especificar el tipo del contenido.")]
        public string MimeType { get; set; }
    }
}
