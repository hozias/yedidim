using System.Linq;
using YedideyChabad.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Web.Security;
using System.Web;
using YedideyChabad.Code;
using System.Collections.Generic;

namespace YedideyChabad.Authentication.MongoDB
{
    public class MongoAuthenticationRepository : IMongoAuthenticationRepository
    {
        private readonly string _connStr;
        private readonly MongoClient _mongoClient;
        private readonly MongoServer _mongoServer;
        private readonly MongoDatabase _mongoDatabase;

        public MongoAuthenticationRepository()
        {
            _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;

            _mongoClient = new MongoClient(_connStr);

            _mongoServer = _mongoClient.GetServer();

            _mongoDatabase = _mongoServer.GetDatabase("yedidim");
        }

        public MongoDatabase GetMongoBatabase() {
            return _mongoDatabase;
        }

        public bool ValidateUser(string user, string password)
        {
            var collection = _mongoDatabase.GetCollection<User>("Users");

            var foundUser = collection.FindOne(Query<User>.Where(s => s.Username == user));
            if (foundUser != null)
            {
                var matched = BCrypt.Net.BCrypt.Verify(password, foundUser.Password);

                HttpContext.Current.Session["_LoggedInUser"] = foundUser;

                return foundUser != null && matched;
            }
            else
            {
                return false;
            }

            
        }

        public void CreateUser(string username, string password)
        {
            const int bcryptWorkFactor = 10;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, bcryptWorkFactor);

            var golduser = new User {Username = username, Password = hashedPassword};
            
            var collection = _mongoDatabase.GetCollection<User>("Users");
            collection.Insert(golduser);
        }


        public string CreateUserAndAccount(string username, string password, IDictionary<string, object> values)
        {
            const int bcryptWorkFactor = 10;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, bcryptWorkFactor);

            var user = new User { Username = username, Password = hashedPassword, Email = values["Email"].ToString(), FirstName = values["FirstName"].ToString(), LastName = values["LastName"].ToString() };
            
            var collection = _mongoDatabase.GetCollection<User>("Users");
            
            collection.Insert(user);

            return user._id.ToString();
        }


        public string[] GetUserRoles(string username)
        {
            var collection = _mongoDatabase.GetCollection<User>("Users");
            var foundUser = collection.FindOne(Query<User>.Where(s => s.Username == username));

            return foundUser.Roles.Select(role => role.Name).ToArray();
        }

        public bool DoUserHaveRole(string username, string rolename)
        {
            var collection = _mongoDatabase.GetCollection<User>("Users");
            var foundUser = collection.FindOne(Query<User>.Where(s => s.Username == username));

            return foundUser.Roles.Exists(role => role.Name == rolename);

        }

        //public MembershipUser GetUser(string username, bool userIsOnline) { 
        
        //}
    }
}