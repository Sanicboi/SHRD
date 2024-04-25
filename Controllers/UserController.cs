using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SHRD
{
    internal static class UserController
    {
        private static HttpClient client = new HttpClient();

        public static async Task Setup(string baseAddress = "http://localhost/") {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("Authorization", AuthorizationController.Token);
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
    }
}
