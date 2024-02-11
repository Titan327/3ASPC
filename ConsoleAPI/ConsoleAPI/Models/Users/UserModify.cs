using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UsersModify
{
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required] 
    public string Pseudo { get; set; } = string.Empty;
    
    [Required]
    public string Password  { get; set; } = string.Empty;
    
    public char Role { get; set; } = '1';
    
}