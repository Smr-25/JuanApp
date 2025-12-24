using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IProductReviewService
{
    Task<List<ProductReviewDto>> GetProductReviewsAsync(int productId);
    Task<ProductReviewDto?> GetReviewByIdAsync(int id);
    Task<bool> CreateReviewAsync(ProductReviewCreateDto dto, string userId, string userName);
    Task<bool> DeleteReviewAsync(int id, string userId);
    Task<double> GetAverageRatingAsync(int productId);
    Task<int> GetReviewCountAsync(int productId);
}

