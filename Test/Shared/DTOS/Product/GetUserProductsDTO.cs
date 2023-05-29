using System.ComponentModel.DataAnnotations;

namespace Test.Shared.DTOS
{
    public class GetUserProductsDTO
    {
        [Required]
        public long UserID { get; set; }
    }
}
