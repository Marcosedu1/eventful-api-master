using Newtonsoft.Json;
using System.Globalization;

namespace eventful_api_master.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cpf { get; set; }
        [JsonProperty("birthdate")]
        private string birthDate { get; set; }
        public int genre { get; set; }
        public bool acceptedTerms { get; set; }
        public bool active { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime changeDate { get; set; }
        public int creationUser { get; set; }
        public int changeUser { get; set; }

        [JsonIgnore]
        public DateTime? birthdate
        {
            get { return DateTime.Parse(birthDate, new CultureInfo("en-US")); }
        }
    }
}
