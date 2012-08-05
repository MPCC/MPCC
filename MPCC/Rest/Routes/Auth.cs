using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
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
            return new GetResponse<Token>() { Entity = new Token() { oauth_timestamp = "2012-08-01 19:00:32.650", oauth_token = "35004A50F47DD74C7C930C2F0B5784B5_51B4580B016FBEAD81ED01056F8311F9" } };
        }

        [WebInvoke(UriTemplate = "v1/passwordreset", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string PasswordReset(string email)
        {
            return "Please check your email and follow the instructions.";
        }

        [WebInvoke(UriTemplate = "v1/registermember", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public GetResponse<Member> RegisterMember(Member entity)
        {
            return new GetResponse<Member>() { Entity = new Member() { FirstName = "First Name", LastName = "Last Name", Email = "someone@gmail.com" } };
        }
    }
}