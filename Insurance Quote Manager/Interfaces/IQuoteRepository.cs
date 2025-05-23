using Insurance_Quote_Manager.Models;

namespace Insurance_Quote_Manager.Interfaces
{
    public interface IQuoteRepository
    {
        Task<List<QuoteModel>> GetAllAsync(string? searchTerm = null);
        Task<QuoteModel?> GetByIdAsync(int id);
        Task AddAsync(QuoteModel quote);
        Task DeleteAsync(QuoteModel quote);
        Task SaveAsync();
    }
}
