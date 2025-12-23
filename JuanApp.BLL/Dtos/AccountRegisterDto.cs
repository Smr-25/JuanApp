using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class AccountRegisterDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    public bool Subscribe { get; set; }
}