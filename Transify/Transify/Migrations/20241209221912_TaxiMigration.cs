using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transify.Migrations
{
    /// <inheritdoc />
    public partial class TaxiMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusRoutes_FromLocation",
                table: "BusRoutes");

            migrationBuilder.DropIndex(
                name: "IX_BusRoutes_FromToLocation",
                table: "BusRoutes");

            migrationBuilder.DropIndex(
                name: "IX_BusRoutes_ToLocation",
                table: "BusRoutes");

            migrationBuilder.DropIndex(
                name: "IX_Buses_Status",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_BusCompanies_CompanyName",
                table: "BusCompanies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusRoutes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Buses",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusCompanies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "BusCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "TaxiCompanies",
                columns: table => new
                {
                    TaxiCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiCompanies", x => x.TaxiCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Taxis",
                columns: table => new
                {
                    TaxiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxiCompanyId = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxis", x => x.TaxiId);
                    table.ForeignKey(
                        name: "FK_Taxis_TaxiCompanies_TaxiCompanyId",
                        column: x => x.TaxiCompanyId,
                        principalTable: "TaxiCompanies",
                        principalColumn: "TaxiCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxiBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxiCompanyId = table.Column<int>(type: "int", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DropoffLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TripStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fare = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TaxiId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_TaxiBookings_TaxiCompanies_TaxiCompanyId",
                        column: x => x.TaxiCompanyId,
                        principalTable: "TaxiCompanies",
                        principalColumn: "TaxiCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxiBookings_Taxis_TaxiId",
                        column: x => x.TaxiId,
                        principalTable: "Taxis",
                        principalColumn: "TaxiId");
                    table.ForeignKey(
                        name: "FK_TaxiBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxiReservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxiId = table.Column<int>(type: "int", nullable: true),
                    TaxiCompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DropoffLocation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TripStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fare = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiReservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_TaxiReservations_TaxiCompanies_TaxiCompanyId",
                        column: x => x.TaxiCompanyId,
                        principalTable: "TaxiCompanies",
                        principalColumn: "TaxiCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxiReservations_Taxis_TaxiId",
                        column: x => x.TaxiId,
                        principalTable: "Taxis",
                        principalColumn: "TaxiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxiReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_TaxiBookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "TaxiBookings",
                        principalColumn: "BookingId");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BookingId",
                table: "Notifications",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiBookings_TaxiCompanyId",
                table: "TaxiBookings",
                column: "TaxiCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiBookings_TaxiId",
                table: "TaxiBookings",
                column: "TaxiId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiBookings_UserId",
                table: "TaxiBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiCompanies_CompanyName",
                table: "TaxiCompanies",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxiReservations_TaxiCompanyId",
                table: "TaxiReservations",
                column: "TaxiCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiReservations_TaxiId",
                table: "TaxiReservations",
                column: "TaxiId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiReservations_UserId",
                table: "TaxiReservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_LicensePlate",
                table: "Taxis",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_TaxiCompanyId",
                table: "Taxis",
                column: "TaxiCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "TaxiReservations");

            migrationBuilder.DropTable(
                name: "TaxiBookings");

            migrationBuilder.DropTable(
                name: "Taxis");

            migrationBuilder.DropTable(
                name: "TaxiCompanies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusRoutes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Buses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BusCompanies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "BusCompanies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_FromLocation",
                table: "BusRoutes",
                column: "FromLocation");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_FromToLocation",
                table: "BusRoutes",
                columns: new[] { "FromLocation", "ToLocation" });

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_ToLocation",
                table: "BusRoutes",
                column: "ToLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_Status",
                table: "Buses",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_BusCompanies_CompanyName",
                table: "BusCompanies",
                column: "CompanyName",
                unique: true);
        }
    }
}
