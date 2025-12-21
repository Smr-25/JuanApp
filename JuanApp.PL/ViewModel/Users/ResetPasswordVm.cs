using System.ComponentModel.DataAnnotations;

namespace JuanApp.PL.ViewModel.Users;

public class ResetPasswordVm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Token { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}