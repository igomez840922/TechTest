using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Shared.Models
{
    [Table("User", Schema = "User")]
    public record User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [Required]
        public long Type { get; set; }
        [MaxLength(64)]
        public string Username { get; set; }
        [MaxLength(320)]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
