using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using SHRD.Models;
using System.Net.Http.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SHRD
{
    internal static class TestController
    {
        private class TestListWrapper
        {
            public List<Test> tests { get; set; }
        }

        
        public static int currentTaskIdx = 0;


        private static HttpClient client = new HttpClient();
        public static async Task Setup(string baseAddress = "http://localhost/")
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthorizationController.Token}");
        }
        public static async Task<List<Test>> GetTests()
        {
            try
            {
                var response = await client.GetAsync("api/tests");
                string data = await response.Content.ReadAsStringAsync();
                TestListWrapper arr = JsonSerializer.Deserialize<TestListWrapper>(data);
                return arr.tests;
            } catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<List<Test>> GetTests(string categoryId)
        {
            var response = await client.GetAsync($"api/tests/categories/{categoryId}");
            string data = await response.Content.ReadAsStringAsync();
            TestListWrapper arr = JsonSerializer.Deserialize<TestListWrapper>(data);
            return arr.tests;
        }

        public static async Task<Test> GetTest(string id)
        {
            var response = await client.GetAsync($"api/tests/{id}");
            var data = await response.Content.ReadAsStringAsync();
            Test test = JsonSerializer.Deserialize<Test>(data);
            return test;
        }
        private class AnswerWrapper
        {
            public List<Answer> answers { get; set; }
        }

        public static async Task<Result> SubmitStats(Test data)
        {
            var url = $"api/test/{data.id}/answers";
            AnswerWrapper d = new AnswerWrapper();
            d.answers = data.tasks.ConvertAll<Answer>(el =>
            {
                Answer a = new Answer();
                a.text = el.answer;
                a.id = el.id;
                return a;
            });
            var response = await client.PostAsJsonAsync(url, d);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Result res = JsonSerializer.Deserialize<Result>(content);
            return res;
        }

        public static async Task CreateTest(Test data)
        {
            try
            {
                var stringContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/test", stringContent);
                response.EnsureSuccessStatusCode();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
