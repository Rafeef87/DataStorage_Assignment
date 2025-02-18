using System;
using System.Collections.Generic;
using DataFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataFirst.Contexts;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<StatusType> StatusTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\ECUtbildning\\Datalagring\\DataStorage_Assignment\\Data\\Databases\\Local_Database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Projects_CustomerId");

            entity.HasIndex(e => e.ProductId, "IX_Projects_ProductId");

            entity.HasIndex(e => e.StatusId, "IX_Projects_StatusId");

            entity.HasIndex(e => e.UserId, "IX_Projects_UserId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Product).WithMany(p => p.Projects).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Status).WithMany(p => p.Projects).HasForeignKey(d => d.StatusId);

            entity.HasOne(d => d.User).WithMany(p => p.Projects).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
