using JuanApp.Core.Models;

namespace JuanApp.Core.Interfaces;

public interface IProductReadRepository
{
    Task<Product?> GetBySlugAsync(string slug);
    Task<IReadOnlyList<ProductImage>> GetGalleryImagesAsync(int productId);
}

