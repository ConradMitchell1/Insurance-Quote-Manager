using Insurance_Quote_Manager.Data;
using Insurance_Quote_Manager.Extensions;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Options;
using Insurance_Quote_Manager.Repository;
using Insurance_Quote_Manager.Services;
using Insurance_Quote_Manager.Services.Strategies;
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
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddQuoteServices();
            builder.Services.AddQuoteConfig(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactDev", policy =>
                {
                    policy.WithOrigins("http://localhost:55469")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            app.UseCors("AllowReactDev");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
