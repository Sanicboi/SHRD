using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using SHRD.Models;

namespace SHRD
{
    internal static class UserController
    {
        public static HttpClient client = new HttpClient();
        private static IRandomAccessStream pic;

        public static async Task Setup(string baseAddress = "http://localhost/") {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthorizationController.Token);
        }

        public static async Task<User> Get(string id)
        {
            var url = "api/users/" + id;
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(body);
            } else
            {
                throw new HttpRequestException();
            }
        }

        public static async Task<User> Me()
        {
            try
            {
                var response = await client.GetAsync("api/me");
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(body);
            } catch (Exception ex)
            {
                return new User();
            }
        }

        public static async Task<IRandomAccessStreamReference> GetPic()
        {
            var response = await client.GetAsync("api/me/pic");
            response.EnsureSuccessStatusCode();
            System.IO.Stream stream = await response.Content.ReadAsStreamAsync();
            pic = stream.AsRandomAccessStream();
            return RandomAccessStreamReference.CreateFromStream(pic);
        }
    }
}
