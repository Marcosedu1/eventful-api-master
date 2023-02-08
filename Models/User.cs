using eventful_api_master.Services;
using eventful_api_master.Utils;
using eventful_api_master.Utils.Converters;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace eventful_api_master.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : Metadata, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [JsonConverter(typeof(LowerCase))]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Range(1,2)]
        public int Genre { get; set; }

        [Required]
        public bool AcceptedTerms { get; set; } = false;

        public void HashPassword(string keyHash)
        {
            Password = Security.HashPassword(keyHash, Password);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            var cpfValidating = new CPFValidate();
            var cpfValidated = cpfValidating.ValidateCPF(Cpf);

            if (!cpfValidated)
            {
                result.Add(new ValidationResult("CPF Inválido"));
            }

            if (BirthDate < DateTime.Now.AddYears(-100) || BirthDate > DateTime.Now.AddYears(-13))
            {
                result.Add(new ValidationResult("Data de Nascimento Inválida"));
            }
            return result;
        }
    }
}
