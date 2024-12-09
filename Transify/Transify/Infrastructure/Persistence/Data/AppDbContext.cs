using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Transify.Infrastructure.Persistence.Data
{
    using global::Transify.Domain.Models.Entities;
    using global::Transify.Infrastructure.Persistence.Configurations;
    using Microsoft.EntityFrameworkCore;

    namespace Transify.Infrastructure.Persistence.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
            public DbSet<BusCompany> BusCompanies { get; set; }
            public DbSet<Buses> Buses { get; set; }

            public DbSet<User> Users { get; set; }
            public DbSet<BusRoutes> BusRoutes { get; set; }
            public DbSet<BusRouteAssignments> BusRouteAssignments { get; set; }
            public DbSet<BusSchedule> BusSchedules { get; set; }
            public DbSet<BusReservations> BusReservations { get; set; }
            // Add DbSet properties for your entities
            // Example:

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new BusCompanyConfiguration());
                modelBuilder.ApplyConfiguration(new BusesConfiguration());
                modelBuilder.ApplyConfiguration(new BusRoutesConfiguration());
                modelBuilder.ApplyConfiguration(new BusRouteAssignmentsConfiguration());
                modelBuilder.ApplyConfiguration(new UserConfiguration());
                modelBuilder.ApplyConfiguration(new BusScheduleConfiguration());
                modelBuilder.ApplyConfiguration(new BusReservationsConfiguration());


                ApplyGlobalQueryFilters(modelBuilder);
            }
            private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<User>()
                  .HasQueryFilter(u => !u.IsDeleted);

                modelBuilder.Entity<BusCompany>()
                  .HasQueryFilter(b => !b.IsDeleted);
            }
        }
    }

}
