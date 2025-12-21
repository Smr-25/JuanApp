using JuanApp.Core.Models;
using JuanApp.Domain.Models;

namespace JuanApp.PL.ViewModel;

public class ShopVm
{
    public List<Product> Products { get; set; }
    public List<Category> Categories { get; set; }
    public List<Size> Sizes { get; set; }
    public List<Color> Colors { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    
}