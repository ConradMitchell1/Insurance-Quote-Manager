using Insurance_Quote_Manager.Models.Enums;

namespace Insurance_Quote_Manager.Models.DTO
{
    public class QuoteListItem
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int ClientAge { get; set; }
        public int CoverageDurationMonths { get; set; }
        public string Email { get; set; } = string.Empty ;
        public PolicyType PolicyType { get; set; }
        public decimal TotalPremium { get; set; }
        public QuoteStatus Status { get; set; }
        public DateTime StartDate { get; set; }
    }
}
