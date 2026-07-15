using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Interfaces;

namespace JuanApp.BLL.Services;

public sealed class ProductService(IProductReadRepository productRepository) : IProductService
{
    public async Task<ProductDetailsDto?> GetBySlugAsync(string slug)
    {
        var product = await productRepository.GetBySlugAsync(slug);
        if (product is null)
        {
            return null;
        }

        var galleryImages = (await productRepository.GetGalleryImagesAsync(product.Id))
            .Select(x => new ProductGalleryDto(x.Id, x.ProductId, x.ImageUrl, x.SortOrder))
            .ToList();

        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Slug,
            product.Description,
            product.ImageUrl,
            product.Price,
            product.OldPrice,
            product.Rating,
            product.InStock,
            product.IsFeatured,
            product.IsNew,
            product.SortOrder,
            galleryImages);
    }
}
