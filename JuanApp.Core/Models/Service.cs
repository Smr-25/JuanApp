using JuanApp.Core.Common;

namespace JuanApp.Domain.Models;

public class Service : BaseEntity
{
    public string Icon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}