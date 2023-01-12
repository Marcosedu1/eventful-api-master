using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace eventful_api_master.Models
{
    public class UserLogin
    {
        [JsonProperty]
        [Required]
        public string Email { get; set; }

        [JsonProperty]
        [Required]
        public string Password { get; set; }
    }
}
