namespace Business.Dtos;

public class ProjectDetailsDto
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CustomerName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
}