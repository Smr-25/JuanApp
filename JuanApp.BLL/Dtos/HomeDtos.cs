namespace JuanApp.BLL.Dtos;

public sealed record HeroSlideDto(
    int Id,
    string Subtitle,
    string Title,
    string Description,
    string BackgroundImageUrl,
    string CtaLabel,
    string CtaUrl);

public sealed record ServiceCardDto(
    int Id,
    string IconUrl,
    string Title,
    string Description,
    string StyleVariant);

public sealed record BannerDto(
    int Id,
    string ImageUrl,
    string Subtitle,
    string Title,
    string TargetUrl,
    int SortOrder,
    string BannerType);

public sealed record BlogPostDto(
    int Id,
    string Title,
    string Slug,
    string CoverImageUrl,
    string Author,
    DateTime PublishedAt);

public sealed record BrandDto(
    int Id,
    string Name,
    string LogoUrl,
    string WebsiteUrl,
    int SortOrder);

public sealed record NewProductsSectionDto(
    BannerDto? SideBanner,
    IReadOnlyList<ProductDto> Products);

public sealed record HomeResponseDto(
    IReadOnlyList<HeroSlideDto> HeroSlides,
    IReadOnlyList<ServiceCardDto> ServiceCards,
    IReadOnlyList<ProductDto> FeaturedProducts,
    IReadOnlyList<BannerDto> StatsBanners,
    NewProductsSectionDto NewProducts,
    IReadOnlyList<BlogPostDto> BlogPosts,
    IReadOnlyList<BrandDto> Brands);

