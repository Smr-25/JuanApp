namespace JuanApp.BLL.Dtos;

public class CheckoutDto
{
    public string? FullName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;
    public bool Subscribe { get; set; }
}

