using JuanApp.Core.Common;

namespace JuanApp.Core.Models;

public class Subscriber : BaseEntity
{
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime SubscribedDate { get; set; } = DateTime.UtcNow;
}

