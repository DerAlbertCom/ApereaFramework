using System;
using System.Security.Cryptography;
using System.Text;

namespace Aperea.Services
{
    public class Hashing : IHashing
    {
        public string GetHash(string text, string salt)
        {
            var hasher = SHA1.Create();
            return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(text + salt)));
        }
    }
}