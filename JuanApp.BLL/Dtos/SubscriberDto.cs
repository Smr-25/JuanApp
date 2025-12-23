using System.ComponentModel.DataAnnotations;

namespace JuanApp.BLL.Dtos;

public class SubscriberDto
{
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public bool IsActive { get; set; }
    public DateTime SubscribedDate { get; set; }
}

