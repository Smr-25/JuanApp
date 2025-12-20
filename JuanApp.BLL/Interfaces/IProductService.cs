using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IProductService
{
    Task<HomeProductDto> GetAllProductsAsync();
}