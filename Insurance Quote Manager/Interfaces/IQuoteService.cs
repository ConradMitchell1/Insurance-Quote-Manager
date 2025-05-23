using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;

namespace Insurance_Quote_Manager.Interfaces
{
    public interface IQuoteService
    {
        Task<List<QuoteListItem>> GetQuotesAsync(string? searchTerm);
        Task CreateQuoteAsync(CreateQuoteRequest request);
        Task UpdateQuoteAsync(UpdateQuoteRequest request);
        Task<QuoteModel> GetQuoteByIdAsync(int id);
        Task DeleteQuoteAsync(int id);

    }
}
