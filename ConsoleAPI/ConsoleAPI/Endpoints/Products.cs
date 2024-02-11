namespace ConsoleAPI.Endpoints;

public class Products
{
    public void getAllUsers(string token = null)
    {
        const string endpoint = "/products/all";

        Program.callApi(token,endpoint,null,"get",null);
    }
    
    public void getUser(string id = "",string token = null)
    {
        const string endpoint = "/products";
        
        string param = "/?id=" + id;
        
        Program.callApi(token,endpoint,null,"get",param);
    }
}