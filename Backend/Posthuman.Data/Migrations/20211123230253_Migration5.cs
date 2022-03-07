using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelExpected",
                table: "RewardCards");

            migrationBuilder.AddColumn<int>(
                name: "RequiredLevel",
                table: "RewardCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredLevel",
                table: "RewardCards");

            migrationBuilder.AddColumn<int>(
                name: "LevelExpected",
                table: "RewardCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
