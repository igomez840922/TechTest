using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NroIdentificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
