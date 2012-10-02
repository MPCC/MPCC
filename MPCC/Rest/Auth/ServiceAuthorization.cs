using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace Rest.Auth
{
    public class ServiceAuthorization : ServiceAuthorizationManager
    {
        public override bool CheckAccess(OperationContext operationContext)
        {
            if(HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("authservice")) { return true; }

            var token = GetToken(operationContext);
            return AuthManager.ValidateToken(token);
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