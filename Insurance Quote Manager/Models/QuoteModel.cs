using Insurance_Quote_Manager.Models.Enums;

namespace Insurance_Quote_Manager.Models
{
    public class QuoteModel
    {
        public int Id { get; set; }

        // Client Details
        public string ClientName { get; set; } = string.Empty;
        public int ClientAge { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsSmoker { get; set; } = false;
        public bool HasChronicIllness { get; set; } = false;

        public string? Location { get; set; } = string.Empty;
        public string? PropertyType { get; set; } = string.Empty ;
        public int? PropertyAge { get; set; }
        public string? ConstructionType { get; set; } = string.Empty;

        // Policy Details
        public PolicyType PolicyType { get; set; } // e.g Home, Auto, Business
        public DateTime StartDate { get; set; }
        public int CoverDurationMonths { get; set; }

        // Financials
        public decimal BasePremium { get; set; }
        public decimal RiskFactor { get; set; }
        public decimal TotalPremium { get; set; }

        //Status & metadata
        public QuoteStatus Status { get; set; } = QuoteStatus.Pending; // Approved, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
