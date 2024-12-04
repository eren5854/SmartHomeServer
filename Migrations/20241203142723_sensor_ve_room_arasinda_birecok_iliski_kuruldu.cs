using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class sensor_ve_room_arasinda_birecok_iliski_kuruldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Rooms_RoomId",
                table: "Sensors");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Rooms_RoomId",
                table: "Sensors",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Rooms_RoomId",
                table: "Sensors");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Rooms_RoomId",
                table: "Sensors",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
