using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Interfaces;
using JuanApp.Core.Models;

namespace JuanApp.BLL.Services;

public sealed class HomeService(IHomeReadRepository homeRepository) : IHomeService
{
    public async Task<HomeResponseDto> GetHomeAsync()
    {
        var heroSlides = (await homeRepository.GetActiveHeroSlidesAsync())
            .Select(x => new HeroSlideDto(x.Id, x.Subtitle, x.Title, x.Description, x.BackgroundImageUrl, x.CtaLabel, x.CtaUrl))
            .ToList();

        var serviceCards = (await homeRepository.GetServiceCardsAsync())
            .Select(x => new ServiceCardDto(x.Id, x.IconUrl, x.Title, x.Description, x.StyleVariant))
            .ToList();

        var featuredProducts = (await homeRepository.GetFeaturedProductsAsync(5))
            .Select(ToProductDto)
            .ToList();

        var statsBanners = (await homeRepository.GetStatsBannersAsync())
            .Select(x => new BannerDto(x.Id, x.ImageUrl, x.Subtitle, x.Title, x.TargetUrl, x.SortOrder, x.BannerType))
            .ToList();

        var sideBanner = await homeRepository.GetSideBannerAsync();
        var newProducts = (await homeRepository.GetNewProductsAsync(6))
            .Select(ToProductDto)
            .ToList();

        var blogPosts = (await homeRepository.GetRecentBlogPostsAsync(4))
            .Select(x => new BlogPostDto(x.Id, x.Title, x.Slug, x.CoverImageUrl, x.Author, x.PublishedAt))
            .ToList();

        var brands = (await homeRepository.GetActiveBrandsAsync())
            .Select(x => new BrandDto(x.Id, x.Name, x.LogoUrl, x.WebsiteUrl, x.SortOrder))
            .ToList();

        return new HomeResponseDto(
            heroSlides,
            serviceCards,
            featuredProducts,
            statsBanners,
            new NewProductsSectionDto(
                sideBanner is null
                    ? null
                    : new BannerDto(sideBanner.Id, sideBanner.ImageUrl, sideBanner.Subtitle, sideBanner.Title, sideBanner.TargetUrl, sideBanner.SortOrder, sideBanner.BannerType),
                newProducts),
            blogPosts,
            brands);
    }

    private static ProductDto ToProductDto(Product x) =>
        new(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.ImageUrl,
            x.Price,
            x.OldPrice,
            x.Rating,
            x.InStock,
            x.IsFeatured,
            x.IsNew,
            x.SortOrder);
}
