﻿using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerUpdateForm
{

    [Key]
    public int Id { get; set; }
    [Required]
    public string CustomerName { get; set; } = null!;
}