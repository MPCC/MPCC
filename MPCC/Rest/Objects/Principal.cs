﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Rest.Data;

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

    public class AuthIdentity : IIdentity
    {
        public string _id { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
    }

    public class AuthUser : IPrincipal
    {
        public IIdentity Identity { get; set; }
        public Principal Principal { get; set; }
        public string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }


    public class Token : BaseObject
    {
        private static string _time;
        public string oauth_token { get;  set; }
        public string oauth_timestamp
        {
            get { return formatToISO86(Convert.ToDateTime(_time));  }
            set { _time = value; }
        }
    }

    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}