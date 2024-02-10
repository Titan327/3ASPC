using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class Carts
{
    [Key]
    public int Id { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public Users User { get; set; }
    
    public int ProductId { get; set; }
    [JsonIgnore]
    public Products Product { get; set; }
    
}