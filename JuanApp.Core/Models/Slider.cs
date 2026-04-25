using JuanApp.Core.Common;

namespace JuanApp.Domain.Models;

public class Slider : BaseEntity
{
    public string? Title { get; set; } 
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? ButtonLink { get; set; }
    public string? ButtonText { get; set; }
    public bool IsMain { get; set; }
}
