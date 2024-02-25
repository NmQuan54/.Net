using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QlBanHang.Helper
{
    internal class AlertHelper
    {

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_-]{3,16}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = @"^[a-zA-Z0-9_-]{6,18}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
        public static bool IsValidRePassword(string password, string rePassword)
        {

            return string.Equals(password, rePassword);
        }
    }
}

