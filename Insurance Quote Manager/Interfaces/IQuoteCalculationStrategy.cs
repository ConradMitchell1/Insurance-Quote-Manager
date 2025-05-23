using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models.Enums;

namespace Insurance_Quote_Manager.Interfaces
{
    public interface IQuoteCalculationStrategy
    {
        PolicyType PolicyType { get; }
        void ApplyCalculation(QuoteModel quote, IQuoteRequest request);
    }
}
