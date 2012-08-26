using System;

namespace Rest.Data
{
    public class BaseRepo
    {
        private const string ISO86Format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

        public static string formatToISO86(DateTime dateTime)
        {
            return dateTime.ToString(ISO86Format);
        }

        public static string formatToISO86(string dateTime)
        {
            var _time = Convert.ToDateTime(dateTime);
            return _time.ToString(ISO86Format);
        }
    }
}