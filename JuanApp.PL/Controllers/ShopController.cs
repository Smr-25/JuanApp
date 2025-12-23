using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class ShopController(
    IProductService productService,
    IColorService colorService,
    ICategoryService categoryService,
    ISizeService sizeService) : Controller
{
    public async Task<IActionResult> Index(
        string search,
        int? categoryId,
        int[] colorIds,
        int[] sizeIds,
        decimal? minPrice,
        decimal? maxPrice,
        string sortBy = "relevance",
        int page = 1
    )
    {
        const int pageSize = 12;
        var filterDto = new ProductFilterDto
        {
            SearchQuery = search,
            CategoryId = categoryId,
            ColorId = colorIds,
            SizeId = sizeIds,
            SortBy = sortBy,
            Page = page,
            PageSize = pageSize,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        var (products, totalCount) = await productService.GetFilteredProductsAsync(filterDto);
        var shopVm = new ShopVm
        {
            Categories = await categoryService.GetAllCategoriesAsync(),
            Colors = await colorService.GetAllColorsAsync(),
            Sizes = await sizeService.GetAllSizesAsync(),
            Products = products.ToList(),
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Filter = filterDto,
            TotalCount = totalCount
        };

        ViewBag.SearchQuery = search;
        ViewBag.HasSearchQuery = !string.IsNullOrWhiteSpace(search);
        return View(shopVm);
    }
}