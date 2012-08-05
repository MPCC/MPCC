using System;
using System.Net;
using System.ServiceModel.Web;
using System.Web.Security;

namespace Rest.Data
{
    public class AuthRepository
    {
        public static Token Login(Login entity)
        {
            if (Membership.ValidateUser(entity.username, entity.password))
            {
                var user = Membership.GetUser(entity.username);
                if (user != null)
                {
                    if (user.ProviderUserKey != null)
                    {
                        var token = AuthManager.GenerateToken(new Guid(user.ProviderUserKey.ToString()), string.Empty, string.Empty);
                        return new Token() { oauth_timestamp = DateTime.Now.ToString(), oauth_token = token};
                    }
                }
            }
            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }
    }
}