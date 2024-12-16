using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class remotecontrol_sinifi_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateSettings_Users_AppUserId",
                table: "TemplateSettings");

            migrationBuilder.DropTable(
                name: "TvCommands");

            migrationBuilder.DropIndex(
                name: "IX_TemplateSettings_AppUserId",
                table: "TemplateSettings");

            migrationBuilder.CreateTable(
                name: "RemoteControls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OnOff = table.Column<bool>(type: "bit", nullable: true),
                    NextChannel = table.Column<bool>(type: "bit", nullable: true),
                    PrevChannel = table.Column<bool>(type: "bit", nullable: true),
                    VolumeUp = table.Column<bool>(type: "bit", nullable: true),
                    VolumeDown = table.Column<bool>(type: "bit", nullable: true),
                    ChannelMenu = table.Column<bool>(type: "bit", nullable: true),
                    Source = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteControls_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_AppUserId",
                table: "TemplateSettings",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteControls_AppUserId",
                table: "RemoteControls",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateSettings_Users_AppUserId",
                table: "TemplateSettings",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateSettings_Users_AppUserId",
                table: "TemplateSettings");

            migrationBuilder.DropTable(
                name: "RemoteControls");

            migrationBuilder.DropIndex(
                name: "IX_TemplateSettings_AppUserId",
                table: "TemplateSettings");

            migrationBuilder.CreateTable(
                name: "TvCommands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChannelMenu = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextChannel = table.Column<bool>(type: "bit", nullable: true),
                    OnOff = table.Column<bool>(type: "bit", nullable: true),
                    PrevChannel = table.Column<bool>(type: "bit", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolumeDown = table.Column<bool>(type: "bit", nullable: true),
                    VolumeUp = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvCommands_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_AppUserId",
                table: "TemplateSettings",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TvCommands_AppUserId",
                table: "TvCommands",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateSettings_Users_AppUserId",
                table: "TemplateSettings",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
