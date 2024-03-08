using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities.DTO
{
    public class UserAuthenticationRequest
    {

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
   
}
