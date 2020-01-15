using Newtonsoft.Json;
using RentYourHomeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RentYourHomeClient
{
    class Program
    {
        private static string user;
        private static string pass;
        private static HttpClientHandler handler = new HttpClientHandler();

        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get List Owners");
            Console.WriteLine("2) Get List Homes by Owner");
            Console.WriteLine("////CRUD for Homes");
            Console.WriteLine("3) Add new Home");
            Console.WriteLine("4) Update Home");
            Console.WriteLine("5) Delete Home");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ListOwners();
                    return true;
                case "2":
                    ListHomesByOwner();
                    return true;
                case "3":
                    AddHome();
                    return true;
                case "4":
                    UpdateHome();
                    return true;
                case "5":
                    DeleteHome();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        private static void ListOwners()
        {
            var result = APiGetCall("Owners");
            WriteResponse(result.Result);
        }

        private static void ListHomesByOwner()
        {
            Console.WriteLine("Enter owner Id.-");
            string ownerId = Console.ReadLine();

            var result = APiGetCall("Homes/"+ownerId+ "/HomesByOwner");
            WriteResponse(result.Result);
        }

        public static void AddHome()
        {
            Console.WriteLine("Enter Json.-");
            string json = Console.ReadLine();

            var result = APiRegisterCall("Homes",json);
            WriteResponse(result.Result);
        }

        public static void UpdateHome()
        {
            Console.WriteLine("Enter Json.-");
            string json = Console.ReadLine();

            Console.WriteLine("Enter HomeId");
            string homeId = Console.ReadLine();

            var result = APiUpdateCall("Homes?id=" + homeId, json);
            WriteResponse(result.Result);
        }

        public static void DeleteHome()
        {
            Console.WriteLine("Enter HomeId");
            string homeId = Console.ReadLine();

            var result = APiDeleteCall("Homes?id=" + homeId);
            WriteResponse(result.Result);
        }

        private static void GetCredentials()
        {
            if(String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pass))
            {
                Console.Clear();
                Console.WriteLine("Please enter credentials to continue.");
                Console.WriteLine("User.-"); user = Console.ReadLine();
                Console.WriteLine("Password.-"); pass = Console.ReadLine();
            }
        }

        private async static Task<HttpResponseMessage> APiGetCall(string endpoint)
        {
            HttpClient client = new HttpClient(handler);

            GetCredentials();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",
                        Convert.ToBase64String(Encoding.Default.GetBytes(user + ":" + pass)));

            return await client.GetAsync(new Uri("https://localhost:44349/api/" + endpoint));
        }

        private async static Task<HttpResponseMessage> APiRegisterCall(string endpoint, string json)
        {
            HttpClient client = new HttpClient(handler);

            GetCredentials();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",
                        Convert.ToBase64String(Encoding.Default.GetBytes(user + ":" + pass)));
            client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.PostAsync(new Uri("https://localhost:44349/api/" + endpoint), new StringContent(json, Encoding.UTF8, "application/json"));

        }

        private async static Task<HttpResponseMessage> APiUpdateCall(string endpoint, string json)
        {
            HttpClient client = new HttpClient(handler);

            GetCredentials();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",
                        Convert.ToBase64String(Encoding.Default.GetBytes(user + ":" + pass)));
            client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.PutAsync(new Uri("https://localhost:44349/api/" + endpoint), new StringContent(json, Encoding.UTF8, "application/json"));

        }

        private async static Task<HttpResponseMessage> APiDeleteCall(string endpoint)
        {
            HttpClient client = new HttpClient(handler);

            GetCredentials();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization",
                        Convert.ToBase64String(Encoding.Default.GetBytes(user + ":" + pass)));
            client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.DeleteAsync(new Uri("https://localhost:44349/api/" + endpoint));

        }


        private static void WriteResponse(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Done " + result.StatusCode);
                var JsonContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JsonContent);
            }
            else
            {
                Console.WriteLine("Error" + result.StatusCode);
            }
           
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }

}

