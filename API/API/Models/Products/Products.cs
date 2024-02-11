using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Org.BouncyCastle.X509;

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
    [JsonIgnore]
    public Users User { get; set; }
    [JsonIgnore]
    public IList<Carts> Carts { get; set; } = new List<Carts>();
}