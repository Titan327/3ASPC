using System;
using System.Text;
using DotNetEnv;

namespace ConsoleAPI
{
    public class Program
    {
        public static void Main()
        {
            string userInput;
            
            Console.WriteLine("\n  ___  ______ _____                     \n / _ \\ | ___ \\_   _|                    \n/ /_\\ \\| |_/ / | |     __ _ _ __  _ __  \n|  _  ||  __/  | |    / _` | '_ \\| '_ \\ \n| | | || |    _| |_  | (_| | |_) | |_) |\n\\_| |_/\\_|    \\___/   \\__,_| .__/| .__/ \n                           | |   | |    \n                           |_|   |_|    \n");
            
            Console.WriteLine("1. Crée un compte");
            Console.WriteLine("2. Se connecter");
            Console.WriteLine("3. Etre Annonyme");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    
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