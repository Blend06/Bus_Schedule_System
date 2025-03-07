﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Entities;

namespace Transify.Configurations
{
    public class BusReservationsConfiguration : IEntityTypeConfiguration<BusReservations>
    {
        public void Configure(EntityTypeBuilder<BusReservations> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureDefaults(builder);
            ConfigureRelationships(builder);
        }

        private void ConfigureKeys(EntityTypeBuilder<BusReservations> builder)
        {
            builder.HasKey(br => br.ReservationId);
        }

        private void ConfigureProperties(EntityTypeBuilder<BusReservations> builder)
        {
            builder.Property(br => br.ReservationId)
                .ValueGeneratedOnAdd();

            builder.Property(br => br.ReservationDate)
                .IsRequired();

            builder.Property(br => br.TotalAmount).HasPrecision(18, 2)
                .IsRequired();

            builder.Property(br => br.PaymentStatus)
                .IsRequired();

            builder.Property(br => br.NumberOfSeats)
                .IsRequired();

            builder.Property(br => br.Status)
                .IsRequired();

            builder.Property(br => br.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(br => br.UpdatedAt)
                .IsRequired(false);
        }

        private void ConfigureDefaults(EntityTypeBuilder<BusReservations> builder)
        {
            builder.Property(br => br.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void ConfigureRelationships(EntityTypeBuilder<BusReservations> builder)
        {
            builder.HasOne(br => br.Schedule)
                .WithMany()
                .HasForeignKey(br => br.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(br => br.User)
                .WithMany(u => u.BusReservations)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(br => br.BusCompany)
                    .WithMany(b => b.BusReservations)
                    .HasForeignKey(br => br.BusCompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
