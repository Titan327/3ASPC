using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UsersCreate
{
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required] 
    public string Pseudo { get; set; } = string.Empty;
    
    [Required]
    public string Password  { get; set; } = string.Empty;
}