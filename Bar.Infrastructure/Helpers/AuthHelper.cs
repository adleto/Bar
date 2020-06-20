using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Bar.Infrastructure.Helpers
{
    public static class AuthHelper
    {
        public static string GenerateHash(string passwordSalt, string password)
        {
            byte[] source = Convert.FromBase64String(passwordSalt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] destination = new byte[source.Length + bytes.Length];

            Buffer.BlockCopy(source, 0, destination, 0, source.Length);
            Buffer.BlockCopy(bytes, 0, destination, source.Length, bytes.Length);

            return Convert.ToBase64String(HashAlgorithm.Create("SHA256").ComputeHash(destination));
        }

        public static string GenerateSalt()
        {
            var buffer = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
    }
}
