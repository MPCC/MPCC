using System;
using System.Web.Script.Serialization;
using System.Xml;

namespace Rest
{
    public static class Utility
    {
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