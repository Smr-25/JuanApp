using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IAdminProductService
{
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductUpdateDto?> GetProductForEditAsync(int id);
    Task<bool> CreateProductAsync(ProductCreateDto dto);
    Task<bool> UpdateProductAsync(ProductUpdateDto dto);
    Task<bool> DeleteProductAsync(int id);
}

