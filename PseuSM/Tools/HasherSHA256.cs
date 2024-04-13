using System.Security.Cryptography;
using System.Text;
using Tools.Abstractions;

namespace Tools
{
    public class HasherSHA256 : IHasher
    {
        public string Hash(string str)
        {
            return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(str)));
        }
    }
}
