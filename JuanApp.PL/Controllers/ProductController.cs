using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class ProductController(IProductDetailsService productDetailsService,IProductService productService) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        var productVm = new ProductVm
        {
            ProductDetails = await productDetailsService.GetProductDetailsAsync(id)
        };
        return View(productVm);
    }
    
    public async Task<IActionResult> ProductModal(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return PartialView("_ProductModalPartial", product);
    }
}