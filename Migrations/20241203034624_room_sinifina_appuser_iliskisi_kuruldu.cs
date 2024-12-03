using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class room_sinifina_appuser_iliskisi_kuruldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AppUserId",
                table: "Rooms",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_AppUserId",
                table: "Rooms",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_AppUserId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AppUserId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Rooms");
        }
    }
}
