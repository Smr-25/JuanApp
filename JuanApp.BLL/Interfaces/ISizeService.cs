using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ISizeService
{
    Task<List<ProductSizeDto>> GetAllSizesAsync();
}