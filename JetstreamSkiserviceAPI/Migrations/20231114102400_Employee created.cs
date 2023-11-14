using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetstreamSkiserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Employeecreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Attempts", "Password", "Username" },
                values: new object[] { 1, 0, "password", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);
        }
    }
}
