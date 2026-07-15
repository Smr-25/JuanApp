using JuanApp.Core.Models;

namespace JuanApp.BLL.Repositories.Interfaces;

public interface IHomeReadRepository
{
    Task<IReadOnlyList<HeroSlide>> GetActiveHeroSlidesAsync();
    Task<IReadOnlyList<ServiceCard>> GetServiceCardsAsync();
    Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int take);
    Task<IReadOnlyList<Banner>> GetStatsBannersAsync();
    Task<Banner?> GetSideBannerAsync();
    Task<IReadOnlyList<Product>> GetNewProductsAsync(int take);
    Task<IReadOnlyList<BlogPost>> GetRecentBlogPostsAsync(int take);
    Task<IReadOnlyList<Brand>> GetActiveBrandsAsync();
}

