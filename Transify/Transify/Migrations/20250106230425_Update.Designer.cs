﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transify.Infrastructure.Data;

#nullable disable

namespace Transify.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250106230425_Update")]
    partial class Update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Transify.Domain.Models.Entities.BaseReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasDiscriminator().HasValue("BaseReview");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusCompany", b =>
                {
                    b.Property<int>("BusCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusCompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BusCompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("BusCompanies");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusReservations", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int?>("BusCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("UserId");

                    b.ToTable("BusReservations");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusRoutes", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"));

                    b.Property<int>("BusCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EstimatedDuration")
                        .HasColumnType("time");

                    b.Property<string>("FromLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ToLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("RouteId");

                    b.HasIndex("BusCompanyId");

                    b.ToTable("BusRoutes");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusSchedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("BusCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ScheduleId");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("BusId");

                    b.HasIndex("RouteId");

                    b.ToTable("BusSchedules");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Buses", b =>
                {
                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusId"));

                    b.Property<int>("BusCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("BusNumber")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("BusId");

                    b.HasIndex("BusCompanyId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Taxi", b =>
                {
                    b.Property<int>("TaxiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxiId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TaxiCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TaxiId");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.HasIndex("TaxiCompanyId");

                    b.ToTable("Taxis");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiBookings", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<string>("DropoffLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Fare")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("PassengerCount")
                        .HasColumnType("int");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TaxiCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("TaxiId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TripEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TripStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("TaxiCompanyId");

                    b.HasIndex("TaxiId");

                    b.HasIndex("UserId");

                    b.ToTable("TaxiBookings");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiCompany", b =>
                {
                    b.Property<int>("TaxiCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxiCompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TaxiCompanyId");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("TaxiCompanies");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiReservations", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("DropoffLocation")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Fare")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("PassengerCount")
                        .HasColumnType("int");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("ReservationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TaxiCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("TaxiId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TripEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TripStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("TaxiCompanyId");

                    b.HasIndex("TaxiId");

                    b.HasIndex("UserId");

                    b.ToTable("TaxiReservations");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("BusinessType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.UserReview", b =>
                {
                    b.HasBaseType("Transify.Domain.Models.Entities.BaseReview");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("UserReview");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusCompany", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusReservations", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.BusCompany", "BusCompany")
                        .WithMany("BusReservations")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Transify.Domain.Models.Entities.BusSchedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Transify.Domain.Models.Entities.User", "User")
                        .WithMany("BusReservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BusCompany");

                    b.Navigation("Schedule");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusRoutes", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.BusCompany", "BusCompany")
                        .WithMany("BusRoutes")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusCompany");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusSchedule", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.BusCompany", "BusCompany")
                        .WithMany("BusSchedules")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transify.Domain.Models.Entities.Buses", "Bus")
                        .WithMany("BusSchedules")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Transify.Domain.Models.Entities.BusRoutes", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bus");

                    b.Navigation("BusCompany");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Buses", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.BusCompany", "BusCompany")
                        .WithMany("Buses")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusCompany");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Taxi", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Transify.Domain.Models.Entities.TaxiCompany", "TaxiCompany")
                        .WithMany("Taxis")
                        .HasForeignKey("TaxiCompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("TaxiCompany");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiBookings", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.TaxiCompany", "TaxiCompany")
                        .WithMany("TaxiBookings")
                        .HasForeignKey("TaxiCompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Transify.Domain.Models.Entities.Taxi", "Taxi")
                        .WithMany("TaxiBookings")
                        .HasForeignKey("TaxiId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Transify.Domain.Models.Entities.User", "User")
                        .WithMany("TaxiBookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Taxi");

                    b.Navigation("TaxiCompany");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiCompany", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiReservations", b =>
                {
                    b.HasOne("Transify.Domain.Models.Entities.TaxiCompany", "TaxiCompany")
                        .WithMany("TaxiReservations")
                        .HasForeignKey("TaxiCompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Transify.Domain.Models.Entities.Taxi", "Taxi")
                        .WithMany("TaxiReservations")
                        .HasForeignKey("TaxiId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Transify.Domain.Models.Entities.User", "User")
                        .WithMany("TaxiReservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Taxi");

                    b.Navigation("TaxiCompany");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.BusCompany", b =>
                {
                    b.Navigation("BusReservations");

                    b.Navigation("BusRoutes");

                    b.Navigation("BusSchedules");

                    b.Navigation("Buses");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Buses", b =>
                {
                    b.Navigation("BusSchedules");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.Taxi", b =>
                {
                    b.Navigation("TaxiBookings");

                    b.Navigation("TaxiReservations");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.TaxiCompany", b =>
                {
                    b.Navigation("TaxiBookings");

                    b.Navigation("TaxiReservations");

                    b.Navigation("Taxis");
                });

            modelBuilder.Entity("Transify.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("BusReservations");

                    b.Navigation("TaxiBookings");

                    b.Navigation("TaxiReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
