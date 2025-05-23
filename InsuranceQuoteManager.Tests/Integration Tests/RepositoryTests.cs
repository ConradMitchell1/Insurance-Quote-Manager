using Insurance_Quote_Manager.Data;
using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceQuoteManager.Tests.Integration_Tests
{
    public class RepositoryTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
                
        }

        [Fact]
        public async Task AddAsync_AddsQuoteToDatabase()
        {
            var(context, _, _) = await SetupQuoteInDbAsync();


            var saved = await context.Quotes.FirstOrDefaultAsync();
            Assert.NotNull(saved);
            Assert.Equal("Test", saved.ClientName);
        }

        [Fact]
        public async Task GetAllAsync_SearchTerm_ReturnsFilteredResults()
        {
            var(context, repo, _) = await SetupQuoteInDbAsync();
            await context.Quotes.AddRangeAsync(new List<QuoteModel>
            {
                new QuoteModel { ClientName = "Alice", Email = "alice@example.com" },
                new QuoteModel { ClientName = "Bob", Email = "bob@example.com" }
            });
            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync("alice");

            Assert.Single(result);
            Assert.Equal("Alice", result[0].ClientName);
        }

        [Fact]
        public async Task DeleteAsync_DeletesQuoteFromDatabase()
        {
            var (context, repo, quote) = await SetupQuoteInDbAsync();

            await repo.DeleteAsync(quote);
            await repo.SaveAsync();

            var deleted = await context.Quotes.FirstOrDefaultAsync(q => q.Email == "test@example.com");
            Assert.Null(deleted);
        }

        private async Task<(ApplicationDbContext context, QuoteRepository repo, QuoteModel quote)> SetupQuoteInDbAsync()
        {
            var context = GetInMemoryDbContext();
            var repo = new QuoteRepository(context);

            var quote = new QuoteModel
            {
                ClientName = "Test",
                Email = "test@example.com",
                ClientAge = 30,
                BasePremium = 100
            };

            await repo.AddAsync(quote);
            await repo.SaveAsync();

            return (context, repo, quote);
        }

    }
}
