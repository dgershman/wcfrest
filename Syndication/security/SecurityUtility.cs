using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace versomas.net.services.syndication.security
{
    public static class SecurityUtility
    {
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GetNetSessionAuthToken(long expires, string path, string salt)
        {
            var sb = new StringBuilder();            
            
            sb.Append(expires.ToString());
            sb.Append(path);
            sb.Append(salt);
            return CalculateMD5Hash(sb.ToString()).ToLower();            
        }

        public static long GetExpires(long ttl)
        {
            return CurrentEpoch() + ttl;
        }

        public static long CurrentEpoch()
        {
            return (long) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;            
        }
    }
}
