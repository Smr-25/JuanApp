using Microsoft.AspNetCore.Http;

namespace JuanApp.BLL.Dtos;

public class BlogCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}

