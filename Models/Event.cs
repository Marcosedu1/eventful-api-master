using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace eventful_api_master.Models
{
    public class Event: Metadata, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [MinLength(1)]
        public string Banner { get; set; }

        [Required]
        public DateTime? Datetime { get; set; }

        [Required]
        [StringLength(8)]
        public string Cep { get; set; }

        [Required]
        [MinLength(1)]
        public string City { get; set; }

        [Required]
        [MinLength(1)]
        public string Uf { get; set; }

        [Required]
        [MinLength(1)]
        public string Address { get; set; }

        [Required]
        [MinLength(1)]
        public string Number { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            if (Datetime < DateTime.Now.AddMonths(1) || Datetime > DateTime.Now.AddYears(1))
            {
                result.Add(new ValidationResult("Digite uma data válida"));
            }
            return result;
        }
    }
}
