using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Entities;

namespace Transify.Configurations
{
    public class TaxiReservationsConfiguration : IEntityTypeConfiguration<TaxiReservations>
    {
        public void Configure(EntityTypeBuilder<TaxiReservations> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureDefaults(builder);
            ConfigureRelationships(builder);
        }

        private void ConfigureKeys(EntityTypeBuilder<TaxiReservations> builder)
        {
            builder.HasKey(tr => tr.ReservationId);
        }

        private void ConfigureProperties(EntityTypeBuilder<TaxiReservations> builder)
        {
            builder.Property(tr => tr.ReservationId)
                .ValueGeneratedOnAdd();

            builder.Property(tr => tr.TaxiId)
                .IsRequired(false);

            builder.Property(tr => tr.TaxiCompanyId)
                .IsRequired();

            builder.Property(tr => tr.UserId)
                .IsRequired();

            builder.Property(tr => tr.PickupLocation)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(tr => tr.PassengerCount)
                .IsRequired();

            builder.Property(tr => tr.DropoffLocation)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(tr => tr.ReservationTime)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(tr => tr.TripStartTime)
                .IsRequired(false);

            builder.Property(tr => tr.TripEndTime)
                .IsRequired(false);

            builder.Property(tr => tr.Fare)
                .IsRequired()
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(18, 2);

            builder.Property(tr => tr.Status)
                .IsRequired();

            builder.Property(tr => tr.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void ConfigureDefaults(EntityTypeBuilder<TaxiReservations> builder)
        {
            builder.Property(tr => tr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void ConfigureRelationships(EntityTypeBuilder<TaxiReservations> builder)
        {
            builder.HasOne(tr => tr.User)
                .WithMany(u => u.TaxiReservations)
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tr => tr.Taxi)
                .WithMany(t => t.TaxiReservations)
                .HasForeignKey(tr => tr.TaxiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tr => tr.TaxiCompany)
                .WithMany(tc => tc.TaxiReservations)
                .HasForeignKey(tr => tr.TaxiCompanyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}