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
    //arrumar o banco de dados tirando o camelcase, tirar os jsonproperty e colocar no appsettings função que serializa os campos em camelcase
    //estudar validações na mao no front-end
}