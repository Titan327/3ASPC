using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

public class CartsProductsName
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; }
    [JsonIgnore]
    public Products Product { get; set; }
    
}