using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rest.Data
{
    public class SubjectLines
    {
        public const string FamilyRequest = "You have a family request!";
        public const string FamilyRequestConfirmed = "You have been added to the {0} family!";
    }

    public class MessageLines
    {
        public const string FamilyRequest = "{0} is requesting to join {1}";
        public const string FamilyRequestConfirmed = "Congradulations! You are now in the {0} family.";
    }
}