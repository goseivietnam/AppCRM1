using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppCRM.Validations
{
    public static class ValidationRule
    {
        public const string Required = "Required";
        public const string Email = "Email";
    }

    public static class Validator
    {
        public static bool IsEmpty(string value)
        {
            if (value == null)
            {
                return true;
            }

            var str = value as string;
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsEmail(string value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
