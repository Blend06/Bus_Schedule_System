using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Transify.Filters;
using Transify.Interfaces;
using Transify.Mappings;
using Transify.Models.DTOs;
using Transify.Data;
using Transify.Services;

namespace Transify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("adminsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(AdminProfile));
            builder.Services.AddScoped<ITaxiCompanyService, TaxiCompanyService>();
            builder.Services.AddScoped<ITaxiService, TaxiService>();
            builder.Services.AddScoped<ITaxiReservationService, TaxiReservationService>();
            builder.Services.AddScoped<ITaxiBookingService, TaxiBookingService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IBusCompanyService, BusCompanyService>();
            builder.Services.AddScoped<IBusService, BusService>();
            builder.Services.AddScoped<IBusScheduleService, BusScheduleService>();
            builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
            builder.Services.AddScoped<IBusReservationService, BusReservationService>();
            builder.Services.AddScoped<ISearchBusScheduleService, SearchBusScheduleService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IDriverDashboardService, DriverDashboardService>();

            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<AdminUserDto>(builder.Configuration.GetSection("DefaultAdmin"));

            builder.Services.AddTransient<AdminSetupService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<AdminOnlyFilter>();

            builder.Services.AddScoped<AdminBaseFilter>();

            builder.Services.AddScoped<LoginRequiredFilter>();

            builder.Services.AddScoped<BusinessOnlyFilter>(provider =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return new BusinessOnlyFilter("", httpContextAccessor);
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var adminSetupService = scope.ServiceProvider.GetRequiredService<AdminSetupService>();
                adminSetupService.EnsureAdminUserExists();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseRouting();

            app.UseSession();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
