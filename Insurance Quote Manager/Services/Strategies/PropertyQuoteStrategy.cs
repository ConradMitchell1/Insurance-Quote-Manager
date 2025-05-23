using Insurance_Quote_Manager.Extensions;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models.Enums;
using Insurance_Quote_Manager.Options;
using Microsoft.Extensions.Options;

namespace Insurance_Quote_Manager.Services.Strategies
{
    public class PropertyQuoteStrategy : IQuoteCalculationStrategy
    {
        private readonly PropertyRiskConfig _config;
        public PropertyQuoteStrategy(IOptions<PropertyRiskConfig> config)
        {
            _config = config.Value;
        }
        public PolicyType PolicyType => PolicyType.Home;
        public void ApplyCalculation(QuoteModel quote, IQuoteRequest request)
        {
            quote.BasePremium = _config.BasePremium;

            var location = Normalize(request.Location);
            var construction = Normalize(request.ConstructionType);
            var propertyType = Normalize(request.PropertyType);
            decimal risk = _config.BaseRisk;

            //Location based risk
            if (location.Contains("flood") || location.Contains("coast"))
                risk += _config.FloodRisk;

            else if (location.Contains("high-crime"))
                risk += _config.HighCrimeRisk;

            //Construction based risk
            if (construction == "wood")
                risk += _config.WoodRisk;

            if (construction == "brick")
                risk -= _config.BrickRisk;

            // Property Age
            if (request.PropertyAge > 30)
                risk += _config.PropertyAgeRisk;

            //Property Type
            if (propertyType == "apartment")
                risk -= _config.PropertyTypeRisk;

            quote.RiskFactor = risk;
            quote.CalculateTotalPremium();
        }

        private string Normalize(string? value) => value?.ToLower() ?? string.Empty;
    }
}
