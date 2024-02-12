using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Products
{
    public static void getAllProduct(string token = null)
    {
        const string endpoint = "/products/all";

        Program.callApi(token,endpoint,null,"get",null);
    }
    
    public static void getProduct(string id = "",string token = null,string typeFilter = null,string sortBy = null,string orderBy = null,string limit = null)
    {
        const string endpoint = "/products";

        string param = "/?";
            
        if (typeFilter != null)
        {
            param = param + "typeFilter = " + typeFilter;
        }
        if (sortBy != null)
        {
            param = param + "sortBy = " + sortBy;
        }
        if (orderBy != null)
        {
            param = param + "orderBy = " + orderBy;
        }
        if (limit != null)
        {
            param = param + "limit = " + limit;
        }
        
        Program.callApi(token,endpoint,null,"get",param);
    }
    
    public static void createProduct(string pseudo = null,string image = null,float price = 0,string type = null,string token = null)
    {
        const string endpoint = "/products";
        
        ProductsCreate product = new ProductsCreate
        {
            Name = pseudo,
            Image = image,
            Price = price,
            Type = type
        };
        
        string body = JsonSerializer.Serialize(product);
        
        Program.callApi(token,endpoint,body,"post",null);
        
    }
    
    public static void putProduct(float price,string id = null,string pseudo = null,string image = null,string type = null,string token = null)
    {
        const string endpoint = "/products";
        
        string param = "/?id=" + id;
        
        ProductsCreate product = new ProductsCreate
        {
            Name = pseudo,
            Image = image,
            Price = price,
            Type = type
        };
        
        string body = JsonSerializer.Serialize(product);
        
        Program.callApi(token,endpoint,body,"put",null);
        
    }
    
    public static void deleteProduct(string id = null, string token = null)
    {
        const string endpoint = "/products";
        string param = "/?id=" + id;
        
        Program.callApi(token,endpoint,null,"del",param);
    }
    
    public static void getProductById(string id = "",string token = null)
    {
        const string endpoint = "/products";
        
        string param = "/?id=" + id;
        
        Program.callApi(token,endpoint,null,"get",param);
    }
    
    public static void getProductSearch(string search = "",string token = null)
    {
        const string endpoint = "/products";
        
        string param = "/?search=" + search;
        
        Program.callApi(token,endpoint,null,"get",param);
    }
}