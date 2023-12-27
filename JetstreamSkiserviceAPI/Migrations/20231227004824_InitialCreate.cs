using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JetstreamSkiserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Attempts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pickup_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "PriorityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Attempts", "Password", "Username" },
                values: new object[,]
                {
                    { 1, 0, "password", "admin" },
                    { 2, 0, "password", "employee1" },
                    { 3, 0, "password", "employee2" },
                    { 4, 0, "password", "employee3" },
                    { 5, 0, "password", "employee4" },
                    { 6, 0, "password", "employee5" },
                    { 7, 0, "password", "employee6" },
                    { 8, 0, "password", "employee7" },
                    { 9, 0, "password", "employee8" },
                    { 10, 0, "password", "employee9" },
                    { 11, 0, "password", "employee10" }
                });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "PriorityId", "PriorityName" },
                values: new object[,]
                {
                    { 1, "Tief" },
                    { 2, "Standard" },
                    { 3, "Express" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "ServiceName" },
                values: new object[,]
                {
                    { 1, "Kleiner Service" },
                    { 2, "Grosser Service" },
                    { 3, "Rennski Service" },
                    { 4, "Bindungen montieren und einstellen" },
                    { 5, "Fell zuschneiden" },
                    { 6, "Heisswachsen" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Offen" },
                    { 2, "InArbeit" },
                    { 3, "abgeschlossen" },
                    { 4, "storniert" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "Comment", "Create_date", "Email", "FirstName", "LastName", "Phone", "Pickup_date", "Price", "PriorityId", "ServiceId", "StatusId" },
                values: new object[,]
                {
                    { 1, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4402), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4444), "50", 1, 1, 1 },
                    { 2, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4449), "Tim@mustermann.com", "Tim", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4450), "60", 2, 2, 2 },
                    { 3, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4453), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4454), "70", 3, 3, 3 },
                    { 4, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4457), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4459), "80", 1, 4, 4 },
                    { 5, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4461), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4463), "90", 2, 5, 1 },
                    { 6, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4465), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4467), "100", 3, 6, 2 },
                    { 7, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4470), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4471), "110", 1, 1, 3 },
                    { 8, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4474), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4475), "120", 2, 2, 1 },
                    { 9, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4478), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4479), "130", 3, 3, 2 },
                    { 10, "Testkommentar", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4481), "max@mustermann.com", "Max", "Muster", "0791234567", new DateTime(2023, 12, 27, 1, 48, 24, 358, DateTimeKind.Local).AddTicks(4483), "140", 1, 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PriorityId",
                table: "Registrations",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ServiceId",
                table: "Registrations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_StatusId",
                table: "Registrations",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
