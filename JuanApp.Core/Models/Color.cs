using JuanApp.Core.Common;
using JuanApp.Domain.Models;

namespace JuanApp.Core.Models;

public class Color : BaseEntity
{
    public string Name { get; set; }
    public string HexCode { get; set; }
    public List<Product> Products { get; set; }
    public List<BasketItem> BasketItems { get; set; }
}