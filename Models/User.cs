using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace eventful_api_master.Models
{
    public class User
    {
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("birthdate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("genre")]
        public int Genre { get; set; }

        [JsonProperty("acceptedTerms")]
        public bool AcceptedTerms { get; set; } = false;

        [JsonIgnore]
        public bool Active { get; set; } = true;

        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        [AllowNull]
        public DateTime? ChangeDate { get; set; } = null;

        [JsonIgnore]
        public int CreationUser { get; set; }

        [JsonIgnore]
        [AllowNull]
        public int? ChangeUser { get; set; } = null;
                
    }
}
