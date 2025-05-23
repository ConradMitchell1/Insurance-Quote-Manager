using Insurance_Quote_Manager.Extensions;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Repository;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Net.Mail;

namespace Insurance_Quote_Manager.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _repo;
        private readonly ILogger<QuoteService> _logger;
        private readonly IQuoteCalculationContext _calculationContext;


        public QuoteService(IQuoteRepository repo, IQuoteCalculationContext calculationContext, ILogger<QuoteService> logger) 
        {
            _logger = logger;
            _repo = repo;
            _calculationContext = calculationContext;
        }

        public async Task CreateQuoteAsync(CreateQuoteRequest request)
        {
            var errors = request.ValidateFields();

            if (errors.Any())
            {
                throw new ArgumentException(string.Join(" ", errors));
            }

            QuoteModel newQuote = new QuoteModel();
            newQuote.CopyFrom(request);

            _calculationContext.ApplyCalculation(newQuote, request);
            _calculationContext.CoverCalculation(newQuote, request);
            _logger.LogInformation("Created quote for {Name}, Total: {Premium}", newQuote.ClientName, newQuote.TotalPremium);
            await _repo.AddAsync(newQuote);
            await _repo.SaveAsync();
            
        }

        public async Task DeleteQuoteAsync(int id)
        {
            var existingQuote = await GetQuoteByIdAsync(id);
            await _repo.DeleteAsync(existingQuote);
            _logger.LogInformation("Deleted quote for ID: {Id}, Name: {Name}", existingQuote.Id, existingQuote.ClientName);
            await _repo.SaveAsync();
        }

        public async Task<List<QuoteListItem>> GetQuotesAsync(string? searchTerm)
        {
            var quotes = await _repo.GetAllAsync(searchTerm);
            return quotes.Select(q => new QuoteListItem
            {
                Id = q.Id,
                ClientName = q.ClientName,
                ClientAge = q.ClientAge,
                CoverageDurationMonths = q.CoverDurationMonths,
                Email = q.Email,
                PolicyType = q.PolicyType,
                TotalPremium = q.TotalPremium,
                Status = q.Status,
                StartDate = q.StartDate,

            }).ToList();
        }

        public async Task<QuoteModel> GetQuoteByIdAsync(int id)
        {
            var quote = await _repo.GetByIdAsync(id);
            if (quote == null) 
            {
                _logger.LogWarning("Quote with ID: {Id} not found", id);
                throw new InvalidOperationException($"Quote with ID: {id} not found.");
            }
            return quote;
        }


        public async Task UpdateQuoteAsync(UpdateQuoteRequest request)
        {
            var errors = request.ValidateFields();
            if (errors.Any())
            {
                throw new ArgumentException(string.Join(" ", errors));
            }
            try
            {
                var existingQuote = await GetQuoteByIdAsync(request.Id);

                existingQuote.CopyFrom(request);
                existingQuote.CoverDurationMonths = request.CoverageDurationMonths;

                _calculationContext.ApplyCalculation(existingQuote, request);
                await _repo.SaveAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Failed to update quote for request: {Request}", request);
                throw;  
            }
            
        }

        
    }
}
