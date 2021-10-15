using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosthumanWebApi.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpGained",
                table: "EventItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpGained",
                table: "EventItems");
        }
    }
}
