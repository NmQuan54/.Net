using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QlBanHang
{
    internal class HashPassword
    {
        private static readonly byte[] Salt = Encoding.UTF8.GetBytes("dotnet");

        public static string HashPass(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                byte[] hashedPasswordByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + Convert.ToBase64String(Salt)));
                return Convert.ToBase64String(hashedPasswordByte);
            }
        }
    }
}
