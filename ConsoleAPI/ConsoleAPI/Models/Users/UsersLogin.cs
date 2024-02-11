using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UsersLogin
{
    [Required]
    public string Pseudo { get; set; }
    [Required]
    public string Password  { get; set; }
}