using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models.Enums;
using Insurance_Quote_Manager.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace Insurance_Quote_Manager.Models.DTO
{
    public class CreateQuoteRequest : IQuoteRequest
    {
        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Range(18, 100)]
        public int ClientAge { get; set; }
        [Required]
        public PolicyType PolicyType { get; set; }
        [Required]
        public QuoteStatus QuoteStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Expiry must be in the future.")]
        public DateTime? ExpiryDate { get; set; }

        //Extra Data for Calculations
        
        // Life
        public bool IsSmoker { get; set; } = false;
        public bool HasChronicIllness { get; set; } = false;

        // Property
        public string? Location { get; set; } = string.Empty ;
        public string? PropertyType { get; set; } = string.Empty ;
        public string? ConstructionType { get; set; } = string.Empty ;
        public int? PropertyAge { get; set; }
    }
}
