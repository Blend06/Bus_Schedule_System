using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Transify.Infrastructure.Persistence.Data
{
    using global::Transify.Domain.Models.Entities;
    using global::Transify.Infrastructure.Persistence.Configurations;
    using global::Transify.Infrastructure.Persistence.Configurations.pentasharp.Data.Configurations;
    using Microsoft.EntityFrameworkCore;

    namespace Transify.Infrastructure.Persistence.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
            public DbSet<TaxiCompany> TaxiCompanies { get; set; }
            public DbSet<Taxi> Taxis { get; set; }
            public DbSet<TaxiReservations> TaxiReservations { get; set; }
            public DbSet<TaxiBookings> TaxiBookings { get; set; }
            public DbSet<BusSchedule> BusSchedules { get; set; }
            public DbSet<BusReservations> BusReservations { get; set; }
            public DbSet<BusCompany> BusCompanies { get; set; }
            public DbSet<Buses> Buses { get; set; }
            public DbSet<BusRoutes> BusRoutes { get; set; }
            public DbSet<BaseReview> Reviews { get; set; }
            public DbSet<UserReview> UserReviews { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfiguration(new UserConfiguration());
                modelBuilder.ApplyConfiguration(new TaxiCompanyConfiguration());
                modelBuilder.ApplyConfiguration(new TaxiConfiguration());
                modelBuilder.ApplyConfiguration(new TaxiReservationsConfiguration());
                modelBuilder.ApplyConfiguration(new BusScheduleConfiguration());
                modelBuilder.ApplyConfiguration(new BusReservationsConfiguration());
                modelBuilder.ApplyConfiguration(new TaxiBookingConfiguration());
                
                ApplyGlobalQueryFilters(modelBuilder);
            }
            private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                    .HasQueryFilter(u => !u.IsDeleted);

                modelBuilder.Entity<BusCompany>()
               .HasQueryFilter(b => !b.IsDeleted);

                modelBuilder.Entity<TaxiCompany>()
                   .HasQueryFilter(tc => !tc.IsDeleted);
            }

        }
    }
}