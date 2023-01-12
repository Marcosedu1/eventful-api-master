using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace eventful_api_master.Models
{
    public class Event
    {
        public int Id { get; set; }

        [JsonProperty("title")]
        [Required]
        public string Title { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("banner")]
        [Required]
        public string Banner { get; set; }

        [JsonProperty("datetime")]
        [Required]
        public DateTime Datetime { get; set; }

        [JsonProperty("cep")]
        [Required]
        public string Cep { get; set; }

        [JsonProperty("city")]
        [Required]
        public string City { get; set; }

        [JsonProperty("uf")]
        [Required]
        public string Uf { get; set; }

        [JsonProperty("address")]
        [Required]
        public string Address { get; set; } 

        [JsonProperty("number")]
        [Required]
        public string Number { get; set; }

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
