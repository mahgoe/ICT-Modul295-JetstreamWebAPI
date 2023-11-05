using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetstreamSkiserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changerequiredfieldsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Priority_PriorityId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriorityId",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Priority_PriorityId",
                table: "Registrations",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Priority_PriorityId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PriorityId",
                table: "Registrations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Priority_PriorityId",
                table: "Registrations",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Status_StatusId",
                table: "Registrations",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
