using System;
using System.Globalization;

namespace AppCRM.Validations
{
    public class Utilities
    {
        public static DateTime? GetDateTimeFromString(string value, string format = "dd/MM/yyyy")
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
        }
    }
}
