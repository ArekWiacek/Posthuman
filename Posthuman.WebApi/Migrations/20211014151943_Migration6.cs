using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosthumanWebApi.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedSubtasks",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSubtasks",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedSubtasks",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TotalSubtasks",
                table: "Projects");
        }
    }
}
