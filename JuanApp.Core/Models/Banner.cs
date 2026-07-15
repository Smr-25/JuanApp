using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Banner : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public string BannerType { get; set; } = string.Empty;
}

