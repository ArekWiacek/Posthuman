using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "IsCyclic",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "TodoItemCycleId",
                table: "TodoItem");

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    AvatarId = table.Column<int>(nullable: false),
                    RepetitionPeriod = table.Column<string>(maxLength: 20, nullable: false),
                    WeekDays = table.Column<int>(nullable: true),
                    DayOfMonth = table.Column<int>(nullable: true),
                    NextIstanceDate = table.Column<DateTime>(nullable: false),
                    LastInstanceDate = table.Column<DateTime>(nullable: true),
                    LastCompletedInstanceDate = table.Column<DateTime>(nullable: true),
                    CompletedInstances = table.Column<int>(nullable: false),
                    MissedInstances = table.Column<int>(nullable: false),
                    InstancesStreak = table.Column<int>(nullable: false),
                    MaxInstancesStreak = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habits_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habits_AvatarId",
                table: "Habits",
                column: "AvatarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.AddColumn<bool>(
                name: "IsCyclic",
                table: "TodoItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TodoItemCycleId",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TodoItemCycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedInstances = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstancesStreak = table.Column<int>(type: "int", nullable: false),
                    InstancesToComplete = table.Column<int>(type: "int", nullable: false),
                    IsInfinite = table.Column<bool>(type: "bit", nullable: false),
                    LastCompletedInstanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInstanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MissedInstances = table.Column<int>(type: "int", nullable: false),
                    NextIstanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepetitionPeriod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TodoItemId = table.Column<int>(type: "int", nullable: false),
                    TotalInstances = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItemCycles_TodoItem_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemCycles_TodoItemId",
                table: "TodoItemCycles",
                column: "TodoItemId",
                unique: true);
        }
    }
}
