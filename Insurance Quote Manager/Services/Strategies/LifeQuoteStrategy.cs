using Insurance_Quote_Manager.Extensions;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models.Enums;
using Insurance_Quote_Manager.Options;
using Microsoft.Extensions.Options;

namespace Insurance_Quote_Manager.Services.Strategies
{
    public class LifeQuoteStrategy : IQuoteCalculationStrategy
    {
        private readonly LifeRiskConfig _config;
        public LifeQuoteStrategy(IOptions<LifeRiskConfig> config)
        {
            _config = config.Value;
        }
        public PolicyType PolicyType => PolicyType.Life;
        public void ApplyCalculation(QuoteModel quote, IQuoteRequest request)
        {
            quote.BasePremium = _config.BasePremium;

            decimal risk = _config.BaseRisk;

            if (request.ClientAge < 25)
                risk += _config.YoungAgeRisk;
            else if (request.ClientAge >= 50)
                risk += _config.OlderAgeRisk;

            if (request.IsSmoker)
                risk += _config.SmokerRisk;
            if (request.HasChronicIllness)
                risk += _config.ChronicIllnessRisk;

            quote.RiskFactor = risk;
            quote.CalculateTotalPremium();
        }
    }
}
