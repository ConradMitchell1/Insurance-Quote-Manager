using Insurance_Quote_Manager.Extensions;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models.Enums;
using Insurance_Quote_Manager.Options;
using Microsoft.Extensions.Options;

namespace Insurance_Quote_Manager.Services.Strategies
{
    public class AutoQuoteStrategy : IQuoteCalculationStrategy
    {
        private readonly AutoRiskConfig _config;
        private readonly ILogger<AutoQuoteStrategy> _logger;
        public AutoQuoteStrategy(IOptions<AutoRiskConfig> config, ILogger<AutoQuoteStrategy> logger)
        {
            _logger = logger;
            _config = config.Value;
        }
        public PolicyType PolicyType => PolicyType.Auto;
        public void ApplyCalculation(QuoteModel quote, IQuoteRequest request)
        {
            quote.BasePremium = _config.BasePremium;
            quote.RiskFactor = request.ClientAge switch
            {
                < 25 => _config.YoungAgeRisk,
                > 60 => _config.OlderAgeRisk,
                _ => _config.BaseRisk,
            };
            _logger.LogInformation("RiskFactor is {RiskFactor}", quote.RiskFactor);
            quote.CalculateTotalPremium();
        }


    }
}
