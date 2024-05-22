using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Http.Json;
using Windows.Storage;
using System.IO;
using Windows.ApplicationModel.Contacts;
using SHRD.Models;
using SHRD.Controllers;
using System.Net.Http.Headers;

namespace SHRD
{
    internal static class AuthorizationController
    {
        
        private static HttpClient client = new HttpClient();
        public static string Token;

        private class TokenData
        {
            public string token { get; set; }
        }


        public static async Task Login(string username, string password)
        {
            UserCreateData requestData = new UserCreateData();
            requestData.password = password;
            requestData.username = username;
            var response = await client.PostAsJsonAsync("api/login", requestData);
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TokenData>(content);
            await TokenManager.Set(data.token);
            Token = data.token;
            UserController.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }



        public static async Task Signup(string username, string password)
        {

                UserCreateData requestData = new UserCreateData();
                requestData.password = password;
                requestData.username = username;
                var response = await client.PostAsJsonAsync("api/user", requestData);
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<TokenData>(content);
                await TokenManager.Set(data.token);
                Token = data.token;
                UserController.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

        }

        public static async Task Logout()
        {
            await TokenManager.Delete();
            Token = "";
        }


        public static async Task Setup(string baseAddress = "http://89.111.172.179/")
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
            await UserController.Setup(baseAddress);
            await TestController.Setup(baseAddress);
            TheoryController.Setup(baseAddress);
            try
            {
                //me.SourceDisplayPicture = await UserController.GetPic();
            }
            catch (Exception ex)
            {

            }
        }
        


    }
}
