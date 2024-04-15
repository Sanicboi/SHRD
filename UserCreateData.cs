using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRD
{
    internal class UserCreateData
    {
        public string username;
        public string password;
        public UserCreateData(string username, string password) { 
            this.username = username;
            this.password = password;
        }
    }
}
