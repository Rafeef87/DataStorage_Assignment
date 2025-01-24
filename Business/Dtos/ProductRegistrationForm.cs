using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProductRegistrationForm
{
    [Required]
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
}