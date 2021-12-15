using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedInstances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "Instances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "InstancesStreak",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "MissedInstances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "RepetitionPeriod",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TodoItem");

            migrationBuilder.CreateTable(
                name: "RepetitionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoItemId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    RepetitionPeriod = table.Column<string>(maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Instances = table.Column<int>(nullable: false),
                    CompletedInstances = table.Column<int>(nullable: false),
                    MissedInstances = table.Column<int>(nullable: false),
                    InstancesStreak = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepetitionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepetitionInfo_TodoItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepetitionInfo_TodoItem_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepetitionInfo_ParentId",
                table: "RepetitionInfo",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RepetitionInfo_TodoItemId",
                table: "RepetitionInfo",
                column: "TodoItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepetitionInfo");

            migrationBuilder.AddColumn<int>(
                name: "CompletedInstances",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TodoItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Instances",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstancesStreak",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissedInstances",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepetitionPeriod",
                table: "TodoItem",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TodoItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TodoItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
