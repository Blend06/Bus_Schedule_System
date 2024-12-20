﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transify.Domain.Models.Entities;

namespace Transify.Infrastructure.Persistence.Configurations
{
    public class BusesConfiguration : IEntityTypeConfiguration<Buses>
    {
        public void Configure(EntityTypeBuilder<Buses> builder)
        {
            ConfigureKeys(builder);
            ConfigureProperties(builder);
            ConfigureIndexes(builder);
            ConfigureDefaults(builder);
            ConfigureRelationships(builder);
        }

        private void ConfigureKeys(EntityTypeBuilder<Buses> builder)
        {
            builder.HasKey(b => b.BusId);
        }

        private void ConfigureProperties(EntityTypeBuilder<Buses> builder)
        {
            builder.Property(b => b.BusId)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.BusNumber)
                .IsRequired();

            builder.Property(b => b.Capacity)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.UpdatedAt);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Buses> builder)
        {
            builder.HasIndex(b => b.Status);
        }

        private void ConfigureDefaults(EntityTypeBuilder<Buses> builder)
        {
            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void ConfigureRelationships(EntityTypeBuilder<Buses> builder)
        {
            builder.HasOne(b => b.BusCompany)
                .WithMany(bc => bc.Buses)
                .HasForeignKey(b => b.BusCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
