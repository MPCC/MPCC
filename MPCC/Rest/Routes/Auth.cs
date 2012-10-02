using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;
using Rest.Data;
using Rest.Objects;

namespace Rest.Routes
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    public class AuthService
    {
        [WebInvoke(UriTemplate = "v1/tokenrequest", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Token> RequestToken(Login entity)
        {
            var token = AuthRepository.Login(entity);
            var cookie = new HttpCookie("_mpcc", "OAuth oauth_token=" + token.oauth_token);

            HttpContext.Current.Response.SetCookie(cookie);
            HttpContext.Current.Response.Headers.Add("Authorization", "OAuth oauth_token=" + token.oauth_token);
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization,content-type,applicationid");
            
            return new GetResponse<Token>() { Entity = token };
        }

        [WebInvoke(UriTemplate = "v1/tokenrefresh", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Token> RefreshToken(Token entity)
        {
            return new GetResponse<Token>() { Entity = AuthRepository.RefreshToken(entity) };
        }

        [WebInvoke(UriTemplate = "v1/passwordreset", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string PasswordReset(string email)
        {
            // TODO: 
            return "Please check your email and follow the instructions.";
        }

        [WebInvoke(UriTemplate = "v1/registermember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Token> RegisterMember(Login entity)
        {
            return new GetResponse<Token>() { Entity = AuthRepository.RegisterMember(entity) };
        }

        [WebInvoke(UriTemplate = "v1/logoff", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void Logoff(Token entity)
        {
            var cookie = new HttpCookie("_mpcc", string.Empty);
            HttpContext.Current.Response.SetCookie(cookie);
            AuthRepository.Logoff(entity);
        }
    }
}