using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models.Enums;

namespace Insurance_Quote_Manager.Repository
{
    public class QuoteCalculationContext : IQuoteCalculationContext
    {
        private readonly Dictionary<PolicyType, IQuoteCalculationStrategy> _strategies;

        public QuoteCalculationContext(IEnumerable<IQuoteCalculationStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(s => s.PolicyType);
        }

        public void ApplyCalculation(QuoteModel quote, IQuoteRequest request)
        {
            if (!_strategies.TryGetValue(request.PolicyType, out var strategy))
                throw new NotSupportedException($"No strategy for {request.PolicyType}");
            strategy.ApplyCalculation(quote, request);
        }

        public void CoverCalculation(QuoteModel quote, CreateQuoteRequest request) 
        {
            if(request.ExpiryDate <= quote.StartDate)
            {
                throw new ArgumentException("Expiry date must be after the start date.");
            }
            if (request.ExpiryDate == null)
            {
                throw new ArgumentException("Expiry date is required.");
            }

            var expiry = request.ExpiryDate.Value;
            var months = ((expiry.Year - quote.StartDate.Year) * 12)
           + expiry.Month - quote.StartDate.Month;

            quote.CoverDurationMonths = Math.Max(1, months);
        }


    }
}
