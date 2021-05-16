using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace WPF_cinema
{
    public partial class User
    {
        public User()
        {
            OrderTickets = new HashSet<OrderTicket>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }

        public User(string name, string login, string password)
        {
            this.Name = name;
            this.Login = login;
            this.Password = getHash(password);
        }

        public static string getHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public virtual ICollection<OrderTicket> OrderTickets { get; set; }
    }
}
