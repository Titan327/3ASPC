using System;
using System.Text;
using API.Models;
using ConsoleAPI.Endpoints;
using DotNetEnv;
using Carts = ConsoleAPI.Endpoints.Carts;
using Users = ConsoleAPI.Endpoints.Users;
using Products = ConsoleAPI.Endpoints.Products;

namespace ConsoleAPI
{
    public class Program
    {
        private static string token;
        static async Task Main()
        {
            
            Console.WriteLine("\n ___________  _____   __                    \n|_   _| ___ \\/ _ \\ \\ / /                    \n  | | | |_/ / /_\\ \\ V /    __ _ _ __  _ __  \n  | | | ___ \\  _  |\\ /    / _` | '_ \\| '_ \\ \n _| |_| |_/ / | | || |   | (_| | |_) | |_) |\n \\___/\\____/\\_| |_/\\_/    \\__,_| .__/| .__/ \n                               | |   | |    \n                               |_|   |_|    \n");
            
            await authChoice();

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

                    authChoice();
                    
                    break;
                
                case "2":
                    Console.WriteLine("Pseudo:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    
                    await Auth.login(pseudo,password);
                    
                    break;
                case "3":
                    endpointChoice();
                    break;
            }
            
            Console.WriteLine("Votre token (laisser vide si vous n'en avez pas)");
            token = Console.ReadLine();
            if (token == "")
            {
                token = null;
            }
            
            endpointChoice();
            
        }


        public static void endpointChoice()
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
                    endpointProducts();
                    break;
                case "2":
                    endpointUsers();
                    break;
                case "3":
                    endpointCarts();
                    break;
            }
        }
        
        public static void endpointCarts()
        {
            string userInput;
            
            Console.WriteLine("Quel endpoint utiliser ?");
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
                    Carts.getAllCarts(token);
                    break;
                case "2":
                    Console.WriteLine("Id du produit a ajouter:");
                    id = Console.ReadLine();
                    Carts.AddProductInCart(id,token);
                    break;
                case "3":
                    Console.WriteLine("Id du produit a supprimer:");
                    id = Console.ReadLine();
                    Carts.deleteProductInCart(id,token);
                    break;
            }
        }

        public static void endpointProducts()
        {
            string userInput;
            
            Console.WriteLine("Quel endpoint utiliser ?");
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
                    Products.getProduct(token);
                    break;
                case "2":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    Products.getProductById(id,token);
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
                    
                    Products.getProduct(id,token,type,sortBy,orderBy,limit);
                    break;
                case "4":
                    Console.WriteLine("search:");
                    search = Console.ReadLine();
                    
                    Products.getProductSearch(search,token);
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
                    
                    Products.createProduct(pseudo,image,price,type,type);
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
                    
                    Products.putProduct(price,id,pseudo,image,type,token);
                    break;
                case "7":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    Products.deleteProduct(id,token);
                    break;
            }
        }

        public static void endpointUsers()
        {
            string userInput;
            
            Console.WriteLine("Quel endpoint utiliser ?");
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
                    Users.getAllUsers(token);
                    break;
                case "2":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    Users.getUser(id,token);
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
                    Users.putUser(role, id, token, email, pseudo, password);
                    break;
                case "4":
                    Console.WriteLine("Pseudo:");
                    email = Console.ReadLine();
                    Console.WriteLine("Email:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    Users.putMe(token, email, pseudo, password);
                    break;
                case "5":
                    Console.WriteLine("id de l'user voulu:");
                    id = Console.ReadLine();
                    Users.deleteUser(id,token);
                    break;
                case "6":
                    Users.deleteUser(token);
                    break;
            }
        }
        

        public static async Task callApi(string token,string endpoint,string body,string httpType,string param)
        {
            string apiUrl = "http://localhost:5229/api" + endpoint + param;

            using (var client = new HttpClient())
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
                
                HttpResponseMessage response = null;
                
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
                /*
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("find");
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseData);
                    
                    endpointChoice();
                    
                }
                */

                
                string responseData = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(responseData);


            }
        }
    }
}