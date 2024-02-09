using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class ProductsCreate
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Image { get; set; } = string.Empty;
    
    [Required]
    public float Price  { get; set; } = 0;
    
}