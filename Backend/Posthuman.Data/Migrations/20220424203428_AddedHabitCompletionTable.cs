using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class AddedHabitCompletionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitsCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    HabitId = table.Column<int>(nullable: false),
                    Occured = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitsCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitsCompletions_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitsCompletions_HabitId",
                table: "HabitsCompletions",
                column: "HabitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitsCompletions");
        }
    }
}
