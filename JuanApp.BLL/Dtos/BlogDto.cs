namespace JuanApp.BLL.Dtos;

public class BlogDto
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public int ViewCount { get; set; }
    public string? Category { get; set; } = string.Empty;
    public List<string>? Tags { get; set; } = new();
}

