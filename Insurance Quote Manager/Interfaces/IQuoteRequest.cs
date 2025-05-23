using Insurance_Quote_Manager.Models.Enums;

namespace Insurance_Quote_Manager.Interfaces
{
    public interface IQuoteRequest
    {
        string ClientName { get; }
        int ClientAge { get; }
        string Email { get; }
        PolicyType PolicyType { get; }
        QuoteStatus QuoteStatus { get; }
        bool IsSmoker { get; }
        bool HasChronicIllness { get; }
        string? Location { get; }
        string? PropertyType { get; }
        string? ConstructionType { get; }
        int? PropertyAge { get; }
    }
}
