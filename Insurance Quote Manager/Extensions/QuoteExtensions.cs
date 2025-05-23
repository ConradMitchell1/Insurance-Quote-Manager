using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Interfaces;
using System.Net.Mail;

namespace Insurance_Quote_Manager.Extensions
{
    public static class QuoteExtensions
    {
        public static void CopyFrom(this QuoteModel quote, IQuoteRequest request)
        {
            quote.ClientName = request.ClientName;
            quote.Email = request.Email;
            quote.PolicyType = request.PolicyType;
            quote.Status = request.QuoteStatus;
            quote.ClientAge = request.ClientAge;
            quote.IsSmoker = request.IsSmoker;
            quote.HasChronicIllness = request.HasChronicIllness;
            quote.Location = request.Location;
            quote.PropertyType = request.PropertyType;
            quote.PropertyAge = request.PropertyAge;
            quote.ConstructionType = request.ConstructionType;
            if(request is CreateQuoteRequest)
            {
                quote.StartDate = DateTime.Now;
            }
            
        }

        public static void CalculateTotalPremium(this QuoteModel quote)
        {
            quote.TotalPremium = quote.BasePremium * quote.RiskFactor;
        }

        public static List<string> ValidateFields(this IQuoteRequest request)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(request.ClientName))
            {
                errors.Add("ClientName is required");
            }


            if (request.ClientAge < 18 || request.ClientAge > 100)
            {
                errors.Add("ClientAge must be at least 18 and below 100");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                errors.Add("Email is required.");
            }

            try
            {
                var addr = new MailAddress(request.Email);
                if (addr.Address != request.Email)
                {
                    errors.Add("Invalid email format.");
                }
            }
            catch
            {
                errors.Add("Invalid email format.");
            }

            if (request is CreateQuoteRequest createRequest)
            {
                if (createRequest.ExpiryDate <= DateTime.Now)
                {
                    errors.Add("Expiry date must be in the future.");
                }

                if(createRequest.ExpiryDate == null)
                {
                    errors.Add("Please Select a date.");
                }
            }

            return errors;
        }
    }
}
