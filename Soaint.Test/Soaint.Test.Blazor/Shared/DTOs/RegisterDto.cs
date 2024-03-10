using System.ComponentModel.DataAnnotations;

namespace Soaint.Test.Blazor.Shared.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Invalid Length", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
    public string? ConfirmPassword { get; set; }
}