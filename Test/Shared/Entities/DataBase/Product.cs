using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Shared.Entities.DataBase
{
    public class Product
    {
        class product
        {

        }
        [Key]
        [Required]
        [MaxLength(37)]
        public string? Id { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Description { get; set; }
        [Required]
        public string? Price { get; set; }
        [Required]
        [DisplayName("upload Image")]
        public string? Photo { get; set; }
    }
}
