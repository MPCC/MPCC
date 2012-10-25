using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;
using Rest.Objects;

namespace Rest.Auth
{
    public class ServiceAuthorization : ServiceAuthorizationManager
    {
        public static AuthUser CurrentUser;

        public override bool CheckAccess(OperationContext operationContext)
        {
            if(HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("authservice")) { return true; }

            var token = GetToken(operationContext);

            if(AuthManager.ValidateToken(token))
            {
                var principal = AuthManager.GetPrincipal(token);
                var roles = Roles.GetRolesForUser(principal.Username);
                
                CurrentUser = new AuthUser()
                {
                    Identity = new AuthIdentity()
                    {
                        IsAuthenticated = true,
                        Name = principal.Username,
                        _id = principal.MemberID.ToString()
                    },
                    Principal = principal,
                    Roles = roles
                };

                return true;
            }

            return false;
        }

        public string GetToken(OperationContext operationContext)
        {
            if(!String.IsNullOrEmpty(HttpContext.Current.Request.Headers["oauth_token"]))
            {
                return HttpContext.Current.Request.Headers["oauth_token"];  
            }


            if(!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["oauth_token"]))
            {
                return HttpContext.Current.Request.QueryString["oauth_token"];
            }

            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }
    }
}