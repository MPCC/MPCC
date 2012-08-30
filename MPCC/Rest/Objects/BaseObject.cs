using System;
using System.Runtime.Serialization;

namespace Rest.Objects
{
    [DataContract]
    public class BaseObject
    {
        private const string ISO86Format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        
        public static string formatToISO86(DateTime dateTime)
        {
            return dateTime.ToString(ISO86Format);
        }
    }
}