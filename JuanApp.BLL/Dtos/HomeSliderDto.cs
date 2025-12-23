﻿using JuanApp.Domain.Models;

namespace JuanApp.BLL.Dtos;

public class HomeSliderDto
{
    // For Home page
    public List<Slider> MainSliders { get; set; } = new();
    public List<Slider> Sliders { get; set; } = new();
    
    // For Admin panel CRUD
    public int Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string? Description { get; set; }
    public string ImageUrl { get; set; }
    public string? ButtonLink { get; set; }
    public string? ButtonText { get; set; }
    public bool IsMain { get; set; }
}
