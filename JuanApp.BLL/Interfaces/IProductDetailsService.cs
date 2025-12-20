using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IProductDetailsService
{
    Task<ProductDetailsDto> GetProductDetailsAsync(int productId);
}