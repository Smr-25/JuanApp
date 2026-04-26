using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class AccountLoginDto
{
    [Required] 
    public string? UsernameOrEmail { get; set; }
    [Required]
    [DataType(DataType.Password)] 
    public string? Password { get; set; }
    
    public bool RememberMe { get; set; }
}