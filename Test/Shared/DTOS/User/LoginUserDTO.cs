using System.ComponentModel.DataAnnotations;

namespace Test.Shared.DTOS
{
    public record LoginUserDTO
    {
        [Required(ErrorMessage = "El correo electronico es requerido.")]
        [EmailAddress( ErrorMessage = "Debe ingresar una dirección de correo valida.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(8, ErrorMessage = "La contraseña debe ser de 8 caracteres como minimo.")]
        public string Password { get; set; }
    }
}
