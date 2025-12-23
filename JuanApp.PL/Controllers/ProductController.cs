using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly AppDbContext _context;

    public ProductController(IProductService productService, AppDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    public async Task<IActionResult> Details(int id)
    {
        var productVm = new ProductVm
        {
            ProductDetails = await _productService.GetProductDetailsAsync(id)
        };
        return View(productVm);
    }

    public async Task<IActionResult> ProductModal(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return PartialView("_ProductModalPartial", product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User not authenticated" });
            }

            var review = new ProductReview
            {
                ProductId = reviewDto.ProductId,
                UserId = userId,
                UserName = User.Identity?.Name ?? "Anonymous",
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Review submitted successfully" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Failed to submit review: " + ex.Message });
        }
    }
}

public class ReviewDto
{
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}