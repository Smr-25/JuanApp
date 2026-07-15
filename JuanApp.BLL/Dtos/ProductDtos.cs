namespace JuanApp.BLL.Dtos;

public sealed record ProductGalleryDto(
    int Id,
    int ProductId,
    string ImageUrl,
    int SortOrder);

public sealed record ProductDto(
    int Id,
    string Name,
    string Slug,
    string? Description,
    string ImageUrl,
    decimal Price,
    decimal? OldPrice,
    decimal Rating,
    bool InStock,
    bool IsFeatured,
    bool IsNew,
    int SortOrder);

public sealed record ProductDetailsDto(
    int Id,
    string Name,
    string Slug,
    string? Description,
    string ImageUrl,
    decimal Price,
    decimal? OldPrice,
    decimal Rating,
    bool InStock,
    bool IsFeatured,
    bool IsNew,
    int SortOrder,
    IReadOnlyList<ProductGalleryDto> GalleryImages);

