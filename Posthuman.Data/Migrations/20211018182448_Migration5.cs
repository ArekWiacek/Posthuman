using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posthuman.Data.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelected",
                table: "Avatars",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Avatars",
                newName: "IsSelected");
        }
    }
}
