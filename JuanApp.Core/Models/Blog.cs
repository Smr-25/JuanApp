using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Blog : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
    public int ViewCount { get; set; } = 0;
    public string Category { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}

