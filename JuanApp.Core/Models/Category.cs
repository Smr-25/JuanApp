using JuanApp.Core.Common;
using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}