using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRD.Models
{
    public class Test
    {
        public string id { get; set; }
        public string name { get; set; }
        public string course { get; set; }
        public List<TestTask> tasks { get; set; }
        
    }
}
