﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    
    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage = "Value must be in range 1 .. 100")]
    public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
