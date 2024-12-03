using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class sensor_tablosuna_secret_key_eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecretKey",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "Sensors");
        }
    }
}
