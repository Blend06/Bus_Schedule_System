﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Domain.Models.Entities;

namespace Transify.Infrastructure.Persistence.Configurations
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
        }

        private void ConfigureProperties(EntityTypeBuilder<BusCompany> builder)
        {
            builder.Property(bc => bc.BusCompanyId)
                .ValueGeneratedOnAdd();

            builder.Property(bc => bc.CompanyName)
                .IsRequired();

            builder.Property(bc => bc.ContactInfo)
                .IsRequired();

            builder.Property(bc => bc.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(bc => bc.UpdatedAt);
        }

        private void ConfigureIndexes(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasIndex(bc => bc.CompanyName)
                .IsUnique();
        }

        private void ConfigureDefaults(EntityTypeBuilder<BusCompany> builder)
        {
            builder.Property(bc => bc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
