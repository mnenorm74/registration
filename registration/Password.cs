using System;
using System.Text;
using System.Security.Cryptography;

namespace registration
{
    class Password
    {
        public static string CalculateMD5Hash(string password)
        {
            return CalculateHash(password, new MD5CryptoServiceProvider());
        }
        public static string CalculateHash(string password, HashAlgorithm algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public static bool IsEqualPasswords(string password, string comparedHash)
        {
            return CalculateMD5Hash(password) == comparedHash;
        }
    }
}
