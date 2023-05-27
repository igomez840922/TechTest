using System.ComponentModel.DataAnnotations;

namespace Test.Shared.DTOS
{
    public record RegisterUserDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [MaxLength(64, ErrorMessage = "El nombre de usuario no debe tener más de 64 caracteres.")]
        [MinLength(1, ErrorMessage = "El nombre de usuario debe tener al menos 1 caracter.")]
        [RegularExpression(@"^[^,:*?""<>\|]*$", ErrorMessage = "El nombre no debe contener caracteres especiales.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El correo electronico es requerido.")]
        [EmailAddress(ErrorMessage = "Introduza una dirección de correo valida.")]
        [MaxLength(320, ErrorMessage = "El correo electronico no debe tener más de 320 caracteres.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MaxLength(64, ErrorMessage = "La contraseña no debe tener más de 64 caracteres.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Debe repetir su contraseña.")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordRepeat { get; set; }
        [Required(ErrorMessage = "El tipo de usuario es requerido")]
        public Models.Type Type { get; set; }
    }
}
