using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rest.Objects
{
    public class Principal
    {
        public int EnterpriseID { get; set; }
        public int BusinessUnitID { get; set; }
        public int MemberID { get; set; }
        public Guid ProviderUserKey { get; set; }
        public string Username { get; set; }
    }

    public class Token
    {
        public string oauth_token { get; set; }
        public string oauth_timestamp { get; set; }
    }

    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}