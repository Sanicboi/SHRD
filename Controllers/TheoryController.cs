using SHRD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SHRD.Models;
using System.Text.Json;
using System.Net.Http.Json;

namespace SHRD.Controllers
{
    public static class TheoryController
    {
        private class TheoryWrapper
        {
            public List<Theory> theory { get; set; }
        }
        private static HttpClient client = new HttpClient();
        public static void Setup(string baseAddress)
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthorizationController.Token}");
        }

        public static async Task<List<Theory>> GetTheory()
        {
            var response = await client.GetAsync("api/theory");
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TheoryWrapper>(data).theory;
        }

        public static async Task CreateTheory(string name, string text, string course)
        {
            var theory = new Theory();
            theory.name = name;
            theory.text = text;
            theory.course = course;
            var response = await client.PostAsJsonAsync("api/theory", theory);
            response.EnsureSuccessStatusCode();
        }
    }
}
