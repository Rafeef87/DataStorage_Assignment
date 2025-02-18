﻿using System;
using System.Collections.Generic;

namespace DataFirst.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
