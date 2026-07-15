using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class HeroSlide : BaseEntity
{
    public string Subtitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public string CtaLabel { get; set; } = string.Empty;
    public string CtaUrl { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

