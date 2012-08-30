using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
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
            return new GetResponse<Token>() { Entity = AuthRepository.Login(entity) };
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
        public static void Logoff(Token entity)
        {
            AuthRepository.Logoff(entity);
        }
    }
}