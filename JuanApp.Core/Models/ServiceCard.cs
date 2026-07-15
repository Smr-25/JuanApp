using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class ServiceCard : BaseEntity
{
    public string IconUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StyleVariant { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

