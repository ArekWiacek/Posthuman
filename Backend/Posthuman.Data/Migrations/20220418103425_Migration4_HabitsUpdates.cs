using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration4_HabitsUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstancesStreak",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "MaxInstancesStreak",
                table: "Habits");

            migrationBuilder.AlterColumn<int>(
                name: "WeekDays",
                table: "Habits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayOfMonth",
                table: "Habits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentInstancesStreak",
                table: "Habits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Habits",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LongestInstancesStreak",
                table: "Habits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentInstancesStreak",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "LongestInstancesStreak",
                table: "Habits");

            migrationBuilder.AlterColumn<int>(
                name: "WeekDays",
                table: "Habits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DayOfMonth",
                table: "Habits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "InstancesStreak",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxInstancesStreak",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
