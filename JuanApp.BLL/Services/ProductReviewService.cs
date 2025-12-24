using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class ProductReviewService(AppDbContext context) : IProductReviewService
{
    public async Task<List<ProductReviewDto>> GetProductReviewsAsync(int productId)
    {
        return await context.ProductReviews
            .Where(r => r.ProductId == productId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ProductReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                UserName = r.UserName,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ProductReviewDto?> GetReviewByIdAsync(int id)
    {
        var review = await context.ProductReviews.FindAsync(id);
        if (review == null) return null;

        return new ProductReviewDto
        {
            Id = review.Id,
            ProductId = review.ProductId,
            UserId = review.UserId,
            UserName = review.UserName,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };
    }

    public async Task<bool> CreateReviewAsync(ProductReviewCreateDto dto, string userId, string userName)
    {
        try
        {
            // Check if user already reviewed this product
            var existingReview = await context.ProductReviews
                .FirstOrDefaultAsync(r => r.ProductId == dto.ProductId && r.UserId == userId);

            if (existingReview != null)
            {
                // Update existing review
                existingReview.Rating = dto.Rating;
                existingReview.Comment = dto.Comment;
                existingReview.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                // Create new review
                var review = new ProductReview
                {
                    ProductId = dto.ProductId,
                    UserId = userId,
                    UserName = userName,
                    Rating = dto.Rating,
                    Comment = dto.Comment,
                    CreatedAt = DateTime.UtcNow
                };
                context.ProductReviews.Add(review);
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteReviewAsync(int id, string userId)
    {
        try
        {
            var review = await context.ProductReviews.FindAsync(id);
            if (review == null || review.UserId != userId)
            {
                return false;
            }

            context.ProductReviews.Remove(review);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<double> GetAverageRatingAsync(int productId)
    {
        var reviews = await context.ProductReviews
            .Where(r => r.ProductId == productId)
            .ToListAsync();

        if (!reviews.Any()) return 0;

        return reviews.Average(r => r.Rating);
    }

    public async Task<int> GetReviewCountAsync(int productId)
    {
        return await context.ProductReviews
            .CountAsync(r => r.ProductId == productId);
    }
}

