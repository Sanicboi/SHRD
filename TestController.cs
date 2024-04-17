using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace SHRD
{
    internal static class TestController
    {
        private class TestListWrapper
        {
            public List<Test> data { get; set; }
            private static HttpClient client = new HttpClient();
            public static async Task Setup(string baseAddress = "http://localhost/")
            {
                client.BaseAddress = new Uri(baseAddress);
            }
            public static async Task<List<Test>> GetTests()
            {
                var response = await client.GetAsync("api/tests");
                string data = await response.Content.ReadAsStringAsync();
                TestListWrapper arr = JsonSerializer.Deserialize<TestListWrapper>(data);
                return arr.data;
            }

            public static async Task<Test> GetTest(string id)
            {
                var response = await client.GetAsync($"api/tests/{id}");
                var data = await response.Content.ReadAsStringAsync();
                Test test = JsonSerializer.Deserialize<Test>(data);
                return test;
            }


        }
    }
}
