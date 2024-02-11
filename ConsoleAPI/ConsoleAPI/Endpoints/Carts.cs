using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Carts
{
    public void getAllCarts(string token = null)
    {
        const string endpoint = "/carts/all";

        Program.callApi(token,endpoint,null,"get",null);
    }
    
    public void AddProductInCart(string id = "",string token = null)
    {
        const string endpoint = "/carts";
        
        string param = "/?productId=" + id;
        
        Program.callApi(token,endpoint,null,"post",param);
    }
    
    public void deleteProductInCart(string id = null, string token = null)
    {
        const string endpoint = "/carts";
        string param = "/?cartItemId=" + id;
        
        Program.callApi(token,endpoint,null,"del",param);
    }
}