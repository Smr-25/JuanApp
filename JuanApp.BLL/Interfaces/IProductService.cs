using JuanApp.BLL.Dtos;
using JuanApp.Domain.Models;

namespace JuanApp.BLL.Interfaces;

public interface IProductService
{
    Task<HomeProductDto> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task<ProductDetailsDto> GetProductDetailsAsync(int productId);
}