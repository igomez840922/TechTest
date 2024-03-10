using System.ComponentModel.DataAnnotations;

namespace Soaint.Test.Blazor.Shared.DTOs;

public class LoginDto
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}