using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRD
{
    internal class TestTask
    {
        public string text;
        public string id;
        public string testId;

        public TestTask(string text, string id, string testId)
        {
            this.text = text;
            this.id = id;
            this.testId = testId;
        }
    }
}
