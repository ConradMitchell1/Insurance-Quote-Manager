using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;

namespace Insurance_Quote_Manager.Interfaces
{
    public interface IQuoteCalculationContext
    {
        void ApplyCalculation(QuoteModel mode, IQuoteRequest request);
        void CoverCalculation (QuoteModel mode, CreateQuoteRequest request);
    }
}
