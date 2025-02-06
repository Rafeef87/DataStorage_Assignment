
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Shared.Context;

public class DataContext : DbContext 
{
    public DbSet<ProjectEntity> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Filename = projects.db");
    }
}
