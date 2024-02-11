using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Auth
{
    public static void register(string email = null,string pseudo = null,string password = null,string token = null)
    {
        const string endpoint = "/Auth/register";
        
        UsersCreate user = new UsersCreate
        {
            Email = email,
            Pseudo = pseudo,
            Password = password
        };
        
        string body = JsonSerializer.Serialize(user);
        
        Program.callApi(token,endpoint,body,"post",null);
        
    }
    
    public static void login(string pseudo = null,string password = null,string token = null)
    {
        const string endpoint = "/Auth/login";
        
        UsersLogin user = new UsersLogin
        {
            Pseudo = pseudo,
            Password = password
        };
        
        string body = JsonSerializer.Serialize(user);
        
        Program.callApi(token,endpoint,body,"post",null);
        
    }
}