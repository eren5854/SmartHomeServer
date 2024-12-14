using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class templatesettings_sinifi_olusturldu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerLayout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderBg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavHeaderBg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Primary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SidebarBg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SidebarPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SidebarStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateSettings_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateSettings_AppUserId",
                table: "TemplateSettings",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateSettings");
        }
    }
}
