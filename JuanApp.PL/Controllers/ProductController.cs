using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.PL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductReviewService _reviewService;

    public ProductController(IProductService productService, IProductReviewService reviewService)
    {
        _productService = productService;
        _reviewService = reviewService;
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
    public async Task<IActionResult> AddReview([FromBody] ProductReviewCreateDto reviewDto)
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User not authenticated" });
            }

            var userName = User.Identity?.Name ?? "Anonymous";
            var result = await _reviewService.CreateReviewAsync(reviewDto, userId, userName);

            if (result)
            {
                return Json(new { success = true, message = "Review submitted successfully" });
            }

            return Json(new { success = false, message = "Failed to submit review" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Failed to submit review: " + ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteReview(int id)
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "User not authenticated" });
            }

            var result = await _reviewService.DeleteReviewAsync(id, userId);

            if (result)
            {
                return Json(new { success = true, message = "Review deleted successfully" });
            }

            return Json(new { success = false, message = "You can only delete your own reviews" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Failed to delete review: " + ex.Message });
        }
    }
}
   