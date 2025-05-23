using System.ComponentModel.DataAnnotations;

namespace Insurance_Quote_Manager.Models.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateValue) 
            {
                return dateValue > DateTime.Today;
            }
            return false;
        }
    }
}
