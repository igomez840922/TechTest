using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Shared.Models
{
    [Table("Product", Schema = "Product")]
    public record Product
    {
        public long UserID { get; set; }
        public long ID { get; set; }
        [MaxLength(255)]
        public string Model { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string ImageData { get; set; }
        [MaxLength(255)]
        public string MimeType { get; set; }
    }
}
