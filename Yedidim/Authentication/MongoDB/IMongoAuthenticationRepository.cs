using System.Collections.Generic;
using System.Web.Security;
namespace YedideyChabad.Authentication.MongoDB
{
    public interface IMongoAuthenticationRepository
    {
        bool ValidateUser(string user, string password);
        void CreateUser(string username, string password);
        string[] GetUserRoles(string username);
        bool DoUserHaveRole(string username, string rolename);

        string CreateUserAndAccount(string user, string password, IDictionary<string, object> values);
        
        //MembershipUser GetUser(string username, bool userIsOnline);
    }
}