using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace eventful_api_master.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User: Metadata
    {
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("password")]
        public string Password { get; set; } = null!;

        [JsonProperty("cpf")]
        public string Cpf { get; set; } = null!;

        [JsonProperty("birthdate")]
        public DateTime? BirthDate { get; set; } = null!;

        [JsonProperty("genre")]
        public int Genre { get; set; } = 0;

        [JsonProperty("acceptedTerms")]
        public bool AcceptedTerms { get; set; } = false;      
        
    }
}
