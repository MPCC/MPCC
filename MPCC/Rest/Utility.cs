﻿using System;
using System.Net;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using Rest.Auth;
using Rest.Objects;

namespace Rest
{
    public static class Utility
    {
        public static Principal GetContext(IncomingWebRequestContext request)
        {
            var token = request.Headers.Get("oauth_token") ?? String.Empty;

            if (String.IsNullOrEmpty(token))
            {
                if(String.IsNullOrEmpty(request.UriTemplateMatch.QueryParameters["oauth_token"]))
                {
                    throw new WebFaultException(HttpStatusCode.Unauthorized);
                }
                
                token = request.UriTemplateMatch.QueryParameters["oauth_token"];
            }

            if (!AuthManager.ValidateToken(token))
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }

            var principal = AuthManager.GetPrincipal(token);

            return principal;
        }

        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static DateTime ToDateTime(string jsonDate)
        {
            return DateTime.Parse(jsonDate);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}