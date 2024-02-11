using System.Text.Json;
using API.Models;

namespace ConsoleAPI.Endpoints;

public class Users
{
    public static void getAllUsers(string token = null)
    {
        const string endpoint = "/user/all";

        Program.callApi(token,endpoint,null,"get",null);
    }
    
    public static void getUser(string id = "",string token = null)
    {
        const string endpoint = "/user";
        
        string param = "/?id=" + id;
        
        Program.callApi(token,endpoint,null,"get",param);
    }
    
    public static void putUser(char role,string id = "",string token = null,string email = null, string pseudo = null, string password = null)
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
        
        Program.callApi(token,endpoint,body,"put",param);
    }

    public static void putMe(string token = null,string email = null,string pseudo = null,string password = null)
    {
        const string endpoint = "/user/me";

        UsersCreate user = new UsersCreate
        {
            Email = email,
            Pseudo = pseudo,
            Password = password
        };
        string body = JsonSerializer.Serialize(user);
        
        Program.callApi(token,endpoint,body,"put",null);
        
    }
    
    public static void deleteUser(string id = null, string token = null)
    {
        const string endpoint = "/user";
        string param = "/?id=" + id;
        
        Program.callApi(token,endpoint,null,"del",param);
    }
    
    public static void deleteMe(string token = null)
    {
        const string endpoint = "/user/me";
        
        Program.callApi(token,endpoint,null,"del",null);
    }
    
    
    
    
}