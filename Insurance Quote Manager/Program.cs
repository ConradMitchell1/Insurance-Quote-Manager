using Insurance_Quote_Manager.Data;
using Insurance_Quote_Manager.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Insurance_Quote_Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddQuoteServices();
            builder.Services.AddQuoteConfig(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:55469", "https://insurance-quote-api.azurewebsites.net", "https://black-sea-0f3f3b80f.6.azurestaticapps.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            app.UseRouting();
            app.UseCors("AllowFrontend");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
