using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRD.Models
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public List<Test> tests { get; set; }
        public List<TestStats> statistics { get; set; }
    }
}
