using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class AdminLoginDto
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}

