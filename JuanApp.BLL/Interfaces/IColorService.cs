using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface IColorService
{
    Task<List<ProductColorDto>> GetAllColorsAsync();
}