using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRD
{
    internal class Test
    {
        public string name;
        public List<TestTask> tasks;
        public string categoryId;

        public Test(string name, List<TestTask> tasks, string categoryId)
        {
            this.name = name;
            this.tasks = tasks;
            this.categoryId = categoryId;
        }
    }
}
