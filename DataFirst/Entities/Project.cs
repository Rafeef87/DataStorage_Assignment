using System;
using System.Collections.Generic;

namespace DataFirst.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual StatusType Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
