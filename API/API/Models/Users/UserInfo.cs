using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class UsersInfo
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required] 
    public string Pseudo { get; set; } = string.Empty;
    
    [Required] 
    public char Role { get; set; }
    
}