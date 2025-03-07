﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Entities;
using Transify.Models.Enums;

namespace Transify.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureIndexes(builder);
            ConfigureDefaults(builder);
            ConfigureRelationships(builder);
        }

        private void ConfigureKeys(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
        }

        private void ConfigureProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired();

            builder.Property(u => u.CompanyId)
               .IsRequired(false);

            builder.Property(u => u.IsAdmin)
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void ConfigureIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }

        private void ConfigureDefaults(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.IsAdmin)
                .HasDefaultValue(false);
            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
        private void ConfigureRelationships(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.BusReservations)
                .WithOne(br => br.User)
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(u => u.TaxiReservations)
                .WithOne(tr => tr.User)
                .HasForeignKey(tr => tr.UserId);

            builder.HasMany(u => u.TaxiBookings)
                .WithOne(tb => tb.User)
                .HasForeignKey(tb => tb.UserId);

            builder.Property(u => u.BusinessType)
                 .IsRequired()
                 .HasDefaultValue(BusinessType.None);
        }
    }
}