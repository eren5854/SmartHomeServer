using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class remotecontrolkey_sinifi_eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelMenu",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "NextChannel",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "OnOff",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "PrevChannel",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "VolumeDown",
                table: "RemoteControls");

            migrationBuilder.DropColumn(
                name: "VolumeUp",
                table: "RemoteControls");

            migrationBuilder.CreateTable(
                name: "RemoteControlKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyValue = table.Column<bool>(type: "bit", nullable: false),
                    RemoteControlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteControlKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteControlKeys_RemoteControls_RemoteControlId",
                        column: x => x.RemoteControlId,
                        principalTable: "RemoteControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemoteControlKeys_RemoteControlId",
                table: "RemoteControlKeys",
                column: "RemoteControlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemoteControlKeys");

            migrationBuilder.AddColumn<bool>(
                name: "ChannelMenu",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NextChannel",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnOff",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrevChannel",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Source",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VolumeDown",
                table: "RemoteControls",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VolumeUp",
                table: "RemoteControls",
                type: "bit",
                nullable: true);
        }
    }
}
