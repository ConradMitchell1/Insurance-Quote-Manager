namespace Insurance_Quote_Manager.Interfaces
{
    public interface IRiskConfig
    {
        decimal BasePremium { get; set; }
        decimal BaseRisk { get; set; }
    }
}
