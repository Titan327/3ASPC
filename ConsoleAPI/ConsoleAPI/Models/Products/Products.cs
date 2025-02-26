using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Products
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Image { get; set; } = string.Empty;
    
    [Required]
    public float Price  { get; set; } = 0;
    
    [Required]
    public char Available  { get; set; } = '1';
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    public DateTime AddedTime  { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public Users User { get; set; }
    
    public IList<Carts> Carts { get; set; } = new List<Carts>();
}