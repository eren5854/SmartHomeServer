using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeServer.Migrations
{
    /// <inheritdoc />
    public partial class senaryo_test_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Scenarios_ScenarioId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Scenarios_ScenarioId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Triggers_ScenarioId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ScenarioId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ScenarioId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "ScenarioId",
                table: "Triggers",
                newName: "ActionId");

            migrationBuilder.AddColumn<Guid>(
                name: "TriggerId",
                table: "Scenarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_ActionId",
                table: "Triggers",
                column: "ActionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scenarios_TriggerId",
                table: "Scenarios",
                column: "TriggerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenarios_Triggers_TriggerId",
                table: "Scenarios",
                column: "TriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Actions_ActionId",
                table: "Triggers",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scenarios_Triggers_TriggerId",
                table: "Scenarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Triggers_Actions_ActionId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Triggers_ActionId",
                table: "Triggers");

            migrationBuilder.DropIndex(
                name: "IX_Scenarios_TriggerId",
                table: "Scenarios");

            migrationBuilder.DropColumn(
                name: "TriggerId",
                table: "Scenarios");

            migrationBuilder.RenameColumn(
                name: "ActionId",
                table: "Triggers",
                newName: "ScenarioId");

            migrationBuilder.AddColumn<Guid>(
                name: "ScenarioId",
                table: "Actions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_ScenarioId",
                table: "Triggers",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ScenarioId",
                table: "Actions",
                column: "ScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Scenarios_ScenarioId",
                table: "Actions",
                column: "ScenarioId",
                principalTable: "Scenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Triggers_Scenarios_ScenarioId",
                table: "Triggers",
                column: "ScenarioId",
                principalTable: "Scenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
