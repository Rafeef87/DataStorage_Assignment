
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Shared.Context;

public class DataContext : DbContext 
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
}
