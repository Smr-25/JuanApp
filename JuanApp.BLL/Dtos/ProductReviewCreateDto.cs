namespace JuanApp.BLL.Dtos;

public class ProductReviewCreateDto
{
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

