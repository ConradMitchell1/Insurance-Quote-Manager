using Insurance_Quote_Manager.Data;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance_Quote_Manager.Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _context;
        public QuoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(QuoteModel quote)
        {
            await _context.Quotes.AddAsync(quote);
            
        }

        public async Task DeleteAsync(QuoteModel Quote)
        {
            _context.Quotes.Remove(Quote);
            await Task.CompletedTask;
        }

        public async Task<List<QuoteModel>> GetAllAsync(string? searchTerm = null)
        {
            var query = _context.Quotes.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(q =>
                    q.ClientName.ToLower().Contains(searchTerm) ||
                    q.Email.ToLower().Contains(searchTerm) ||
                    q.PolicyType.ToString().ToLower().Contains(searchTerm) ||
                    q.Status.ToString().ToLower().Contains(searchTerm)
                );
            }

            return await query.ToListAsync();
        }

        public async Task<QuoteModel?> GetByIdAsync(int id)
        {
            return await _context.Quotes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
