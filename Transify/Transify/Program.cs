using Transify.Application.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Transify.Application.Services;
using Transify.Domain.Interfaces;
using Transify.Domain.Models.DTOs;
using Transify.Presentation.Filters;
using Transify.Infrastructure.Data;

namespace Transify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Specify the web root path using WebApplicationOptions
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                WebRootPath = "Presentation/wwwroot" // Set the web root path here
            });

            // Add configurations for appsettings.
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Add controllers with views to the service collection.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(AdminProfile));
            builder.Services.AddAutoMapper(typeof(TaxiReservationProfile));
            builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();


            // Add scoped services for authentication.
            builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            // Configure the DbContext with the connection string.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure the AdminUserDto to be bound from configuration.
            builder.Services.Configure<AdminUserDto>(builder.Configuration.GetSection("DefaultAdmin"));

            // Add the AdminSetupService as a transient service.
            builder.Services.AddTransient<AdminSetupService>();

            // Configure session settings.
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set the session timeout.
                options.Cookie.HttpOnly = true;  // Make session cookie HTTP-only for security.
                options.Cookie.IsEssential = true;  // Mark the session cookie as essential.
            });

            // Add HttpContextAccessor to access HTTP context throughout the application.
            builder.Services.AddHttpContextAccessor();

            // Add scoped filters.
            builder.Services.AddScoped<AdminOnlyFilter>();
            builder.Services.AddScoped<AdminBaseFilter>();
            builder.Services.AddScoped<LoginRequiredFilter>();
            builder.Services.AddScoped<BusinessOnlyFilter>(provider =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return new BusinessOnlyFilter("", httpContextAccessor); // Add BusinessOnlyFilter with HttpContextAccessor
            });

            // Build the application.
            var app = builder.Build();

            // Configure middleware for the application.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  // Enforce strict transport security in production.
            }

            // Middleware for HTTPS redirection, static files, authorization, and routing.
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Serve static files from Presentation/wwwroot
            app.UseAuthorization();
            app.UseRouting();

            // Enable session management.
            app.UseSession();

            // Define default route for controllers.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the application.
            app.Run();
        }
    }
}
