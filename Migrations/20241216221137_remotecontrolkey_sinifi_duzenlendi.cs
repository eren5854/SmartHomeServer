using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class remotecontrolkey_sinifi_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyQueue",
                table: "RemoteControlKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyQueue",
                table: "RemoteControlKeys");
        }
    }
}
