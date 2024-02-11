using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Carts
{
    public static void getAllCarts(string token = null)
    {
        const string endpoint = "/carts/all";

        Program.callApi(token,endpoint,null,"get",null);
    }
    
    public static void AddProductInCart(string id = "",string token = null)
    {
        const string endpoint = "/carts";
        
        string param = "/?productId=" + id;
        
        Program.callApi(token,endpoint,null,"post",param);
    }
    
    public static void deleteProductInCart(string id = null, string token = null)
    {
        const string endpoint = "/carts";
        string param = "/?cartItemId=" + id;
        
        Program.callApi(token,endpoint,null,"del",param);
    }
}