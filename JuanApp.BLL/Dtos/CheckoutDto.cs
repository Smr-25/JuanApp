using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class CheckoutDto
{
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone must be between 10 and 20 characters")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(250, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 250 characters")]
    public string? Address { get; set; }

    [StringLength(500, ErrorMessage = "Notes must not exceed 500 characters")]
    public string? Notes { get; set; }

    public bool Subscribe { get; set; }
}

