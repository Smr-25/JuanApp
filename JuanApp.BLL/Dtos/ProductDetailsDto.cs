using JuanApp.Domain.Models;

namespace JuanApp.BLL.Dtos;

public class ProductDetailsDto
{
    public Product Product { get; set; }
    public List<Product> RelatedProducts { get; set; }
}