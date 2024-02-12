using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Users
{
    public static async Task getAllUsers(string token = null)
    {
        const string endpoint = "/user/all";

        await Program.callApi(token,endpoint,null,"get",null);
    }
    
    public static async Task getUser(string id = "",string token = null)
    {
        const string endpoint = "/user";
        
        string param = "/?id=" + id;
        
        await Program.callApi(token,endpoint,null,"get",param);
    }
    
    public static async Task putUser(char role,string id = "",string token = null,string email = null, string pseudo = null, string password = null)
    {
        const string endpoint = "/user";
        
        string param = "/?id=" + id;
        
        UsersModify user = new UsersModify
        {
            Email = email,
            Pseudo = pseudo,
            Password = password,
            Role = role
        };
        string body = JsonSerializer.Serialize(user);
        
        await Program.callApi(token,endpoint,body,"put",param);
    }

    public static async Task putMe(string token = null,string email = null,string pseudo = null,string password = null)
    {
        const string endpoint = "/user/me";

        UsersCreate user = new UsersCreate
        {
            Email = email,
            Pseudo = pseudo,
            Password = password
        };
        string body = JsonSerializer.Serialize(user);
        
        await Program.callApi(token,endpoint,body,"put",null);
        
    }
    
    public static async Task deleteUser(string id = null, string token = null)
    {
        const string endpoint = "/user";
        string param = "/?id=" + id;
        
        await Program.callApi(token,endpoint,null,"del",param);
    }
    
    public static async Task deleteMe(string token = null)
    {
        const string endpoint = "/user/me";
        
        await Program.callApi(token,endpoint,null,"del",null);
    }
    
    
    
    
}