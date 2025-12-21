using System.ComponentModel.DataAnnotations;

namespace JuanApp.PL.ViewModel;

public class ForgotPasswordVm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}