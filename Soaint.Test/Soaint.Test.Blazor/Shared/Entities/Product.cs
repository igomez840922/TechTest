using System.ComponentModel.DataAnnotations;

namespace Soaint.Test.Blazor.Shared.Entities;

public class Product
{
    public int Id { get; set; }
    public Model Model { get; set; }
    public int ModelId { get; set; }
    [Required(ErrorMessage = "Required value")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Required value")]
    public decimal UnitPrice { get; set; }
    public string? Photo { get; set; } //TODO
}