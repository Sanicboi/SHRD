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

namespace SHRD
{
    internal static class AuthorizationController
    {
        
        private static HttpClient client = new HttpClient();
        public static string Token;
        public static Contact me;
        public static User instance;

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

        }

        public static async Task Logout()
        {
            await TokenManager.Delete();
            Token = "";
        }


        public static async Task Setup(string baseAddress = "http://localhost/")
        {
            client.BaseAddress = new Uri(baseAddress);
            me = new Contact();
            Token = "";
            try
            {
                Token = await TokenManager.Get();
                await UserController.Setup();
                await TestController.Setup();
                TheoryController.Setup();
                instance = await UserController.Me();
                me.FirstName = instance.username;
                
            } catch (Exception ex)
            {
                Token = "";
            }
            try
            {
                me.SourceDisplayPicture = await UserController.GetPic();
            }
            catch (Exception ex)
            {

            }
        }
        


    }
}
