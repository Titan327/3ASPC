using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Users
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required] 
    public string Pseudo { get; set; } = string.Empty;
    
    [Required]
    public string Password  { get; set; } = string.Empty;
    
    [Required]
    public char Role { get; set; } = '1';
}