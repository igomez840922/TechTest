using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Shared.Models
{
    [Table("Type", Schema = "User")]
    public record Type
    {
        [Key]
        public long ID { get; set; }
        [StringLength(32)]
        public string Description { get; set; }
    }
}
