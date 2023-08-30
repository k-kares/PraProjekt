using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public User() { }
        public User(string name, string email, string password, bool admin) 
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = admin;
        }
    }

    public class Kolegij
    {
        public string Name { get; set; }
        public string UserName { get; set; }

    }
}
