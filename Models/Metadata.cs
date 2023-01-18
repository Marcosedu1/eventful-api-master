using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace eventful_api_master.Models
{
    public class Metadata
    {
        [JsonIgnore]
        [AllowNull]
        public bool Active { get; set; } = true;

        [JsonIgnore]
        [AllowNull]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        [AllowNull]
        public DateTime? ChangeDate { get; set; } = null;

        [JsonIgnore]
        [AllowNull]
        public int? CreationUser { get; set; } = null;

        [JsonIgnore]
        [AllowNull]
        public int? ChangeUser { get; set; } = null;
    }
}
