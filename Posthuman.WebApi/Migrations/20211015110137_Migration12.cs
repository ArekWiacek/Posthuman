using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosthumanWebApi.Migrations
{
    public partial class Migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelatedEntityId",
                table: "EventItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedEntityType",
                table: "EventItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedEntityId",
                table: "EventItems");

            migrationBuilder.DropColumn(
                name: "RelatedEntityType",
                table: "EventItems");
        }
    }
}
