using System;
using System.Text;
using API.Models;
using ConsoleAPI.Endpoints;
using DotNetEnv;
using Users = ConsoleAPI.Endpoints.Users;

namespace ConsoleAPI
{
    public class Program
    {
        private string token;
        public void Main()
        {
            Console.WriteLine("\n  ___  ______ _____                     \n / _ \\ | ___ \\_   _|                    \n/ /_\\ \\| |_/ / | |     __ _ _ __  _ __  \n|  _  ||  __/  | |    / _` | '_ \\| '_ \\ \n| | | || |    _| |_  | (_| | |_) | |_) |\n\\_| |_/\\_|    \\___/   \\__,_| .__/| .__/ \n                           | |   | |    \n                           |_|   |_|    \n");

            authChoice();
            
            Console.WriteLine("Votre token (laisser vide si vous n'en avez pas)");
            token = Console.ReadLine();

            endpointChoice();

        }



        public void authChoice()
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
                    
                    Auth.register(email,pseudo,password);
                    
                    break;
                
                case "2":
                    Console.WriteLine("Pseudo:");
                    pseudo = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    
                    Auth.login(pseudo,password);
                    
                    break;
                
            }
        }


        public void endpointChoice()
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
                    break;
                case "2":
                    endpointUsers();
                    break;
                case "3":
                    break;
            }
        }

        public void endpointUsers()
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
        

        public static async Task<string> callApi(string token,string endpoint,string body,string httpType,string param)
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
                
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                return "Error : " + response.StatusCode;
                
            }
        }
    }
}