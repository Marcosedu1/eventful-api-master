using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace eventful_api_master.Models
{
    public class Event: Metadata
    {
        public int Id { get; set; }

        [JsonProperty("title")]
        [Required]
        public string Title { get; set; } = null!;

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; } = null!;

        [JsonProperty("banner")]
        [Required]
        public string Banner { get; set; } = null!;

        [JsonProperty("datetime")]
        [Required]
        public DateTime? Datetime { get; set; } = null!;

        [JsonProperty("cep")]
        [Required]
        public string Cep { get; set; } = null!;

        [JsonProperty("city")]
        [Required]
        public string City { get; set; } = null!;

        [JsonProperty("uf")]
        [Required]
        public string Uf { get; set; } = null!;

        [JsonProperty("address")]
        [Required]
        public string Address { get; set; } = null!;

        [JsonProperty("number")]
        [Required]
        public string Number { get; set; } = null!;
    }
}
