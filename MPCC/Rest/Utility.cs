using System;
using System.Net;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;
using System.Xml;
using Auth;

namespace Rest
{
    public static class Utility
    {
        public static Principal GetContext(IncomingWebRequestContext request)
        {
            var token = request.Headers.Get("oauth_token") ?? String.Empty;

            if (String.IsNullOrEmpty(token))
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }

            if (!AuthManager.ValidateToken(token))
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }

            var principal = AuthManager.GetPrincipal(token);

            return principal;
        }

        public static string ToISO86(DateTime date)
        {
            return XmlConvert.ToString(date, XmlDateTimeSerializationMode.Local);
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