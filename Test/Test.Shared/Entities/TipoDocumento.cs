using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
