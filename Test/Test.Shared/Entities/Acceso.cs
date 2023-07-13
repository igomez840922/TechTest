using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public class Acceso
    {
        [Key]
        public int IdAcceso { get; set; }
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? UltimoAcceso { get; set; }
    }
}
