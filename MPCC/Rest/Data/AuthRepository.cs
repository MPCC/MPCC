using System;
using System.Net;
using System.ServiceModel.Web;
using System.Web.Security;
using Rest.Objects;
using Rest.Auth;

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

        public static Token RegisterMember(Login entity)
        {
            var user = Membership.CreateUser(entity.username, entity.password, entity.email);
            if (user.ProviderUserKey != null)
            {
                var token = AuthManager.GenerateToken(new Guid(user.ProviderUserKey.ToString()), string.Empty, string.Empty);
                return new Token() { oauth_timestamp = DateTime.Now.ToString(), oauth_token = token };
            }
            throw new WebFaultException(HttpStatusCode.InternalServerError);
        }

        public static void Logoff(Token entity)
        {
            try
            {
                AuthManager.DisposeToken(entity.oauth_token);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        public static Token RefreshToken(Token entity)
        {
            var principal = AuthManager.GetPrincipal(entity.oauth_token);
            var token = AuthManager.GenerateToken(principal.EnterpriseID, principal.BusinessUnitID, principal.MemberID,
                                                  principal.ProviderUserKey, string.Empty, string.Empty);
            AuthManager.DisposeToken(entity.oauth_token);
            return new Token() { oauth_timestamp = DateTime.Now.ToString(), oauth_token = token };
        }
    }
}