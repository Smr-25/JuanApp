using JuanApp.Core.Common;
using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class Size : BaseEntity
{
    public string Name { get; set; }
    public string SizeType { get; set; }
    public List<Product> Products { get; set; }
}

