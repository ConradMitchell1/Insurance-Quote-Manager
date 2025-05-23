using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
namespace Insurance_Quote_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly IQuoteService _quoteService;
        public QuoteController(ILogger<QuoteController> logger, IQuoteService quoteService)
        {
            _logger = logger;
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotes([FromQuery]string? searchTerm)
        {
            var quotes = await _quoteService.GetQuotesAsync(searchTerm);
            return Ok(quotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody]CreateQuoteRequest quote)
        {
            await _quoteService.CreateQuoteAsync(quote);
            return CreatedAtAction(nameof(GetQuotes), null);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id) 
        {
            await _quoteService.DeleteQuoteAsync(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuote([FromBody]UpdateQuoteRequest quote)
        {
            await _quoteService.UpdateQuoteAsync(quote);
            return NoContent();
        }

    }
}
