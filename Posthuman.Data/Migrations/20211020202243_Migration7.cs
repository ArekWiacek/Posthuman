using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posthuman.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "TodoItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_ParentId",
                table: "TodoItem",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_TodoItem_ParentId",
                table: "TodoItem",
                column: "ParentId",
                principalTable: "TodoItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_TodoItem_ParentId",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_ParentId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TodoItem");
        }
    }
}
