using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class scenario_appuser_iliskisi_kuruldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Scenarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Scenarios_AppUserId",
                table: "Scenarios",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scenarios_Users_AppUserId",
                table: "Scenarios",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scenarios_Users_AppUserId",
                table: "Scenarios");

            migrationBuilder.DropIndex(
                name: "IX_Scenarios_AppUserId",
                table: "Scenarios");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Scenarios");
        }
    }
}
