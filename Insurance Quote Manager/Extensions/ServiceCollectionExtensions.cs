using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Options;
using Insurance_Quote_Manager.Repository;
using Insurance_Quote_Manager.Services;
using Insurance_Quote_Manager.Services.Strategies;
using System.Runtime.CompilerServices;

namespace Insurance_Quote_Manager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQuoteServices(this IServiceCollection services)
        {
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IQuoteCalculationStrategy, AutoQuoteStrategy>();
            services.AddScoped<IQuoteCalculationStrategy, LifeQuoteStrategy>();
            services.AddScoped<IQuoteCalculationStrategy, PropertyQuoteStrategy>();
            services.AddScoped<IQuoteCalculationContext, QuoteCalculationContext>();

        }

        public static void AddQuoteConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LifeRiskConfig>(configuration.GetSection("RiskFactors:Life"));
            services.Configure<AutoRiskConfig>(configuration.GetSection("RiskFactors:Auto"));
            services.Configure<PropertyRiskConfig>(configuration.GetSection("RiskFactors:Property"));
        }
    }
}
