using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using Windows.Storage;
using System.IO;

namespace SHRD
{
    internal class AuthorizationController
    {
        
        private static HttpClient client = new HttpClient();
        public static string Token;

        public static async Task<bool> Login(string username, string password)
        {
            UserCreateData requestData = new UserCreateData(username, password);
            StringContent requestContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/login", requestContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var token = await response.Content.ReadAsStringAsync();
                await TokenManager.Set(token);
                Token = token;
                return true;
            } else
            {
                return false;
            }

        }



        public static async Task Signup(string username, string password)
        {
            UserCreateData requestData = new UserCreateData(username, password);
            StringContent requestContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/user", requestContent);
            string token = await response.Content.ReadAsStringAsync();
            await TokenManager.Set(token);
            Token = token;
        }

        public static async Task Logout()
        {
            await TokenManager.Delete();
            Token = "";
        }


        public static async Task Setup(string baseAddress = "http://localhost/")
        {
            client.BaseAddress = new Uri(baseAddress);
            Token = "";
            try
            {
                Token = await TokenManager.Get();
            } catch (Exception ex)
            {
                Token = "";
            }
        }
        


    }
}
