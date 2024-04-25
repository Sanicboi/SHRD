using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SHRD
{
    internal static class TestController
    {
        private class TestListWrapper
        {
            public List<Test> data { get; set; }
        }

        public static List<string> currentAnswers;
        public static Test currentTest;
        public static int currentTaskIdx = 0;


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

        public static async Task<List<Test>> GetTests(string categoryId)
        {
            var response = await client.GetAsync($"api/tests/categories/{categoryId}");
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

        public static bool Check(Test test, int idx, string answer)
        {
            return test.tasks[idx].answer == answer;
        }

        public static int Check(Test test, List<string> answers)
        {
            if (answers.Count != test.tasks.Count) throw new IndexOutOfRangeException();
            int points = 0;
            for (int i = 0; i < answers.Count; i++)
            {
                if (Check(test, i, answers[i]))
                {
                    points++;
                }
            }
            return points;
        }
    }
}
