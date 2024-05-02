using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoe_Store_DB.Classes
{
    public class User
    {
        private string username;
        private string password;
        private string mode;

        public User(string username, string password, string mode)
        {
            UserName = username;
            Password = password;
            Mode = mode;
        }

        public String UserName
        {
            get { return username; }
            set { username = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
    }
}