using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Insurance_Quote_Manager.Models.DTO
{
    public class UpdateQuoteRequest : IQuoteRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ClientName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Range(18, 100)]
        public int ClientAge { get; set; }
        [Required]
        public PolicyType PolicyType { get; set; }
        [Required]
        public QuoteStatus QuoteStatus { get; set; }
        [Required]
        public int CoverageDurationMonths { get; set; }
        
        public bool IsSmoker { get; set; } = false;
        public bool HasChronicIllness { get; set; } = false;
        public string? Location { get; set; } = string.Empty;
        public string? PropertyType { get; set; } = string.Empty;
        public string? ConstructionType { get; set; } = string.Empty;
        public int? PropertyAge { get; set; }

        
    }
}

