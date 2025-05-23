using Insurance_Quote_Manager.Interfaces;

namespace Insurance_Quote_Manager.Options
{
    public class AutoRiskConfig : IRiskConfig
    {
        public decimal BasePremium { get; set; }
        public decimal YoungAgeRisk { get; set; }
        public decimal OlderAgeRisk { get; set; }
        public decimal BaseRisk { get; set; }
    }
}
