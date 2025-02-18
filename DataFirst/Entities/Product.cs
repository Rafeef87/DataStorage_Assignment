using System;
using System.Collections.Generic;

namespace DataFirst.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
