using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rest.Objects
{
    public class Channel
    {
        public virtual string Type { get; set; }
        public virtual string UserId { get; set; }
    }
}