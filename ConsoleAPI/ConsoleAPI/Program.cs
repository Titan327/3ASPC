using System;
using System.Text;
using API.Models;
using ConsoleAPI.Endpoints;
using DotNetEnv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Carts = ConsoleAPI.Endpoints.Carts;
using Users = ConsoleAPI.Endpoints.Users;
using Products = ConsoleAPI.Endpoints.Products;

namespace ConsoleAPI
{
    public class Program
    {
        private static string token = @"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50a\nXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVGl0YW4zMjciLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZ\nnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiIxIiwiZXhwIjoxNzA4MzcwMDM1fQ.eGpH81YNHyGGkTm3azV2mwkKny7pEmNDf6b5vtFOkw5HoccvaBk30e2j2W07MEMzS8NPI2aUAZq-8LmNSNC7Iw";
        static async Task Main()
        {
            
            Console.WriteLine("\n ___________  _____   __                    \n|_   _| ___ \\/ _ \\ \\ / /                    \n  | | | |_/ / /_\\ \\ V /    __ _ _ __  _ __  \n  | | | ___ \\  _  |\\ /    / _` | '_ \\| '_ \\ \n _| |_| |_/ / | | || |   | (_| | |_) | |_) |\n \\___/\\____/\\_| |_/\\_/    \\__,_| .__/| .__/ \n                               | |   | |    \n                               |_|   |_|    \n");
            
            await authChoice();
            
            Console.WriteLine("End");

        }
        

        public static async Task authChoice()
        {
            string userInput;
            
            Console.WriteLine("1. Crée un compte");
            Console.WriteLine("2. Se connecter");
            Console.WriteLine("3. Passer");

            userInput = Console.ReadLine();

            string email = null;
            string pseudo = null;
            string password = null;
            
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Email:");
                    email = Console.ReadLine();
                    Console.WriteLine("Pseudo:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    
                    await Auth.register(email,pseudo,password);

                    await authChoice();
                    
                    break;
                
                case "2":
                    Console.WriteLine("Pseudo:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    
                    await Auth.login(pseudo,password);
                    
                    await authChoice();
                    
                    break;
                case "3":
                    await endpointChoice();
                    break;
            }
            
            
            
        }


        public static async Task endpointChoice()
        {
            string userInput;
            
            Console.WriteLine("Quel endpoint utiliser ?");
            Console.WriteLine("1. Products");
            Console.WriteLine("2. Users");
            Console.WriteLine("3. Carts");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    await endpointProducts();
                    break;
                case "2":
                    await endpointUsers();
                    break;
                case "3":
                    await endpointCarts();
                    break;
            }
        }
        
        public static async Task endpointCarts()
        {
            string userInput;
            
            Console.WriteLine("Quel route utiliser ?");
            Console.WriteLine("1. get all");
            Console.WriteLine("2. add item in cart");
            Console.WriteLine("3. delete item");
            

            userInput = Console.ReadLine();

            string id = null;
            string email = null;
            string pseudo = null;
            string password = null;

            switch (userInput)
            {
                case "1":
                    await Carts.getAllCarts(token);
                    await endpointChoice();
                    break;
                case "2":
                    Console.WriteLine("Id du produit a ajouter:");
                    id = Console.ReadLine();
                    await Carts.AddProductInCart(id,token);
                    await endpointChoice();
                    break;
                case "3":
                    Console.WriteLine("Id du produit a supprimer:");
                    id = Console.ReadLine();
                    await Carts.deleteProductInCart(id,token);
                    await endpointChoice();
                    break;
            }
        }

        public static async Task endpointProducts()
        {
            string userInput;
            
            Console.WriteLine("Quel route utiliser ?");
            Console.WriteLine("1. get all");
            Console.WriteLine("2. get by id");
            Console.WriteLine("3. get with filter");
            Console.WriteLine("4. search product");
            Console.WriteLine("5. Create product");
            Console.WriteLine("6. Modify product");
            Console.WriteLine("7. Delete product by id");

            userInput = Console.ReadLine();

            string id = null;
            char role;
            string email = null;
            string pseudo = null;
            string password = null;
            string type = null;
            string sortBy = null;
            string orderBy = null;
            string limit = null;
            string search = null;
            string name = null;
            string image = null;
            float price = 0;
            float result = 0;

            switch (userInput)
            {
                case "1":
                    await Products.getProduct(token);
                    await endpointChoice();
                    break;
                case "2":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    await Products.getProductById(id,token);
                    await endpointChoice();
                    break;
                case "3":
                    Console.WriteLine("Laisser vide les champ non voulu");
                    Console.WriteLine("type:");
                    type = Console.ReadLine();
                    Console.WriteLine("trier par:");
                    sortBy = Console.ReadLine();
                    Console.WriteLine("ASC ou DESC:");
                    orderBy = Console.ReadLine();
                    Console.WriteLine("limit:");
                    limit = Console.ReadLine();
                    
                    await Products.getProduct(id,token,type,sortBy,orderBy,limit);
                    await endpointChoice();
                    break;
                case "4":
                    Console.WriteLine("search:");
                    search = Console.ReadLine();
                    
                    await Products.getProductSearch(search,token);
                    await endpointChoice();
                    break;
                case "5":
                    Console.WriteLine("name:");
                    name = Console.ReadLine();
                    Console.WriteLine("image:");
                    image = Console.ReadLine();
                    Console.WriteLine("price:");
                    price = float.TryParse(Console.ReadLine(), out result) ? result : 0;
                    Console.WriteLine("type:");
                    type = Console.ReadLine();
                    
                    await Products.createProduct(pseudo,image,price,type,type);
                    await endpointChoice();
                    break;
                case "6":
                    Console.WriteLine("id de l'user a modifier:");
                    id = Console.ReadLine();
                    
                    Console.WriteLine("name:");
                    name = Console.ReadLine();
                    Console.WriteLine("image:");
                    image = Console.ReadLine();
                    Console.WriteLine("price:");
                    price = float.TryParse(Console.ReadLine(), out result) ? result : 0;
                    Console.WriteLine("type:");
                    type = Console.ReadLine();
                    
                    await Products.putProduct(price,id,pseudo,image,type,token);
                    await endpointChoice();
                    break;
                case "7":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    await Products.deleteProduct(id,token);
                    await endpointChoice();
                    break;
            }
        }

        public static async Task endpointUsers()
        {
            string userInput;
            
            Console.WriteLine("Quel route utiliser ?");
            Console.WriteLine("1. get all");
            Console.WriteLine("2. get by id");
            Console.WriteLine("3. change user by id");
            Console.WriteLine("4. change me");
            Console.WriteLine("5. delete user by id");
            Console.WriteLine("6. delete me");

            userInput = Console.ReadLine();

            string id = null;
            char role;
            string email = null;
            string pseudo = null;
            string password = null;

            switch (userInput)
            {
                case "1":
                    await Users.getAllUsers(token);
                    await endpointChoice();
                    break;
                case "2":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    await Users.getUser(id,token);
                    await endpointChoice();
                    break;
                case "3":
                    Console.WriteLine("Id de l'utilisateur a modifier:");
                    id = Console.ReadLine();
                    Console.WriteLine("Role:");
                    role = Convert.ToChar(Console.ReadLine());
                    Console.WriteLine("Pseudo:");
                    email = Console.ReadLine();
                    Console.WriteLine("Email:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    await Users.putUser(role, id, token, email, pseudo, password);
                    await endpointChoice();
                    break;
                case "4":
                    Console.WriteLine("Pseudo:");
                    email = Console.ReadLine();
                    Console.WriteLine("Email:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    await Users.putMe(token, email, pseudo, password);
                    await endpointChoice();
                    break;
                case "5":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    await Users.deleteUser(id,token);
                    await endpointChoice();
                    break;
                case "6":
                    await Users.deleteUser(token);
                    await endpointChoice();
                    break;
            }
        }
        
        public static async Task callApi(string token,string endpoint,string body,string httpType,string param)
        {
            
            string apiUrl = "http://localhost:5229/api" + endpoint + param;
            
            //using (var client = new HttpClient())
            using (HttpClient client = new HttpClient())
            {
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                StringContent content = null;
                if (body != null)
                {
                    content = new StringContent(body, Encoding.UTF8, "application/json");
                }
                Console.WriteLine(body);
                Console.WriteLine(content);
                //HttpResponseMessage response = null;
                HttpResponseMessage response;
                
                switch (httpType)
                {
                    case "get":
                        response = await client.GetAsync(apiUrl);
                        break;
                    case "post":
                        response = await client.PostAsync(apiUrl, content);
                        break;
                    case "put":
                        response = await client.PutAsync(apiUrl, content);
                        break;
                    case "del":
                        response = await client.DeleteAsync(apiUrl);
                        break;
                    default:
                        throw new ArgumentException("Invalid HTTP type");
                }
                
                string responseData = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(responseData);
                
                
            }
        }
        
    }
    
}