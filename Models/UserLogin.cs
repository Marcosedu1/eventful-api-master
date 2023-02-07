using eventful_api_master.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eventful_api_master.Models
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public void HashPassword(string keyHash)
        {
            Password = Security.HashPassword(keyHash, Password);
        }
    }
    
}
