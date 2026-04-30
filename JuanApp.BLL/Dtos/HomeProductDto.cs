using JuanApp.Domain.Models;

namespace JuanApp.BLL.Dtos;

public class HomeProductDto
{
    public List<Product>? MainProducts { get; set; } = new();
    public List<Product>? NewProducts { get; set; } = new();
}
