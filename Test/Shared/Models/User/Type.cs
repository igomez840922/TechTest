using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Shared.Models
{
    [Table("Type", Schema = "User")]
    public class Type
    {
        [Key]
        public long ID { get; set; }
        [StringLength(32)]
        public string Description { get; set; }

        public override bool Equals(object o)
        {
            var other = o as Type;
            return other?.ID == ID;
        }
        public override int GetHashCode() => Description?.GetHashCode() ?? 0;
        public override string ToString() => Description;
    }
}
