﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Models.Entities;

namespace Transify.Configurations
{
    public class BusCompanyConfiguration : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureIndexes(builder);
            ConfigureDefaults(builder);
        }

        private void ConfigureKeys(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasKey(bc => bc.BusCompanyId);

            builder.HasOne(bc => bc.User)
                   .WithOne()
                   .HasForeignKey<BusCompany>(bc => bc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureProperties(EntityTypeBuilder<BusCompany> builder)
        {
            builder.Property(bc => bc.BusCompanyId)
                .ValueGeneratedOnAdd();

            builder.Property(bc => bc.CompanyName)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(bc => bc.ContactInfo)
                .IsRequired();

            builder.Property(bc => bc.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(bc => bc.UpdatedAt)
                .IsRequired(false);
        }

        private void ConfigureIndexes(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasIndex(bc => bc.CompanyName)
                .IsUnique();

            builder.HasIndex(bc => bc.UserId)
                .IsUnique();
        }

        private void ConfigureRelationships(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasMany(bc => bc.Buses)
                .WithOne(b => b.BusCompany)
                .HasForeignKey(b => b.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(bc => bc.BusRoutes)
               .WithOne(b => b.BusCompany)
               .HasForeignKey(b => b.BusCompanyId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureDefaults(EntityTypeBuilder<BusCompany> builder)
        {
            builder.Property(bc => bc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
