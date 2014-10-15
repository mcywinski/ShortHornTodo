using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ShortHorn.Helpers
{
    /// <summary>
    /// Contains validators for common fields used in all projects
    /// </summary>
    public class Validation
    {
        public static bool IsNotEmpty(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            else return true;
        }

        public static bool IsNotNull(object value)
        {
            if (value == null) return false;
            else return true;
        }

        public static bool IsEmailAddress(string value)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);
            if (match.Success) return true;
            else return false;
        }

        public static bool AreEqual(object value, object compareValue)
        {
            if (value.Equals(compareValue)) return true;
            else return false;
        }

        public static bool IsLongerEqual(string value, int minimum)
        {
            if (value.Length >= minimum) return true;
            else return false;
        }
    }
}