﻿using JuanApp.Domain.Models;

namespace JuanApp.BLL.Dtos;

public class HomeAdvantageDto
{
    // For Home page
    public List<Advantage>? Advantages { get; set; } = new();
    
    // For Admin panel CRUD
    public int Id { get; set; }
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
