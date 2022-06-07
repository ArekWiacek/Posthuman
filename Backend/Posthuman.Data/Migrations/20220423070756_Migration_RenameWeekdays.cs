using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration_RenameWeekdays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDays",
                table: "Habits");

            migrationBuilder.AddColumn<int>(
                name: "DaysOfWeek",
                table: "Habits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOfWeek",
                table: "Habits");

            migrationBuilder.AddColumn<int>(
                name: "WeekDays",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
