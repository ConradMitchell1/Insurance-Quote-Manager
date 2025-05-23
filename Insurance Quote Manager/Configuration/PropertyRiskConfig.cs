using Insurance_Quote_Manager.Interfaces;

namespace Insurance_Quote_Manager.Options
{
    public class PropertyRiskConfig : IRiskConfig
    {
        public decimal BasePremium { get; set; }
        public decimal BaseRisk { get; set; }
        public decimal FloodRisk { get; set; }
        public decimal HighCrimeRisk { get; set; }
        public decimal WoodRisk { get; set; }
        public decimal BrickRisk { get; set; }
        public decimal PropertyAgeRisk { get; set; }
        public decimal PropertyTypeRisk { get; set; }
    }
}
