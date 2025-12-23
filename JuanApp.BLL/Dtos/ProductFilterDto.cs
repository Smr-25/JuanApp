namespace JuanApp.BLL.Dtos;

public class ProductFilterDto
{
    public string? SearchQuery { get; set; }
    public int? CategoryId { get; set; }
    public int[] SizeId { get; set; }
    public int[] ColorId { get; set; }
    public string SortBy { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice {get; set; } 
}

