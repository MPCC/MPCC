using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rest
{
    public class Principal
    {
        public int EnterpriseID { get; set; }
        public int BusinessUnitID { get; set; }
        public int MemberID { get; set; }
        public Guid ProviderUserKey { get; set; }
        public string Username { get; set; }
    }
}