using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string ProjectName { get; set; } = null!;
    [Required]

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public int CustomerId { get; set; }
    public CustomerRegistrationForm Customer { get; set; } = null!;

    public int StatusId { get; set; }
    public StatusTypeRegistrationForm Status { get; set; } = null!;

    public int UserId { get; set; }
    public UserRegistrationForm User { get; set; } = null!;

    public int ProductId { get; set; }
    public ProductRegistrationForm Product { get; set; } = null!;
}