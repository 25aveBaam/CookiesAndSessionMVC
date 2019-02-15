using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookiesAndSession.Models
{
    public class User
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public List<User> UserList = new List<User>();

        public List<User> ReturnList()
        {
            UserList.Add(new User()
            {
                Id = 1,
                UserName = "Pozu",
                Password = "1234",
                FirstName = "David",
                LastName = "Pozuelo"
            });

            return UserList;
        }
    }
}