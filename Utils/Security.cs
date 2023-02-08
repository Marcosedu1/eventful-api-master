using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace eventful_api_master.Utils
{
    public class Security
    {
        public static string HashPassword(string keyHash, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Encoding.Unicode.GetBytes(keyHash),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }
    }
}