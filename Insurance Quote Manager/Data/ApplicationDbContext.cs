using Insurance_Quote_Manager.Models;
using Insurance_Quote_Manager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Insurance_Quote_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<QuoteModel> Quotes { get; set; }



    }
}
