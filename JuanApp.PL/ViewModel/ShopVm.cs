using JuanApp.BLL.Dtos;
using JuanApp.Core.Models;
using JuanApp.Domain.Models;

namespace JuanApp.PL.ViewModel;

public class ShopVm
{
    public List<ProductDto> Products { get; set; }
    public List<ProductCategoryDto> Categories { get; set; }
    public List<ProductSizeDto> Sizes { get; set; }
    public List<ProductColorDto> Colors { get; set; }
    public ProductFilterDto Filter { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    
    
}