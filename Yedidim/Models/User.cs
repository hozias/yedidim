using MongoDB.Bson;
using System.Collections.Generic;

namespace YedideyChabad.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}