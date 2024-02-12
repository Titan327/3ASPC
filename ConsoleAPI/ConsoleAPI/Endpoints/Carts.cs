using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Carts
{
    public static async Task getAllCarts(string token = null)
    {
        const string endpoint = "/carts/all";

        await Program.callApi(token,endpoint,null,"get",null);
    }
    
    public static async Task AddProductInCart(string id = "",string token = null)
    {
        const string endpoint = "/carts";
        
        string param = "/?productId=" + id;
        
        Program.callApi(token,endpoint,null,"post",param);
    }
    
    public static async Task deleteProductInCart(string id = null, string token = null)
    {
        const string endpoint = "/carts";
        string param = "/?cartItemId=" + id;
        
        await Program.callApi(token,endpoint,null,"del",param);
    }
}