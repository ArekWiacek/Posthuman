using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posthuman.Data.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "TodoItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_AvatarId",
                table: "TodoItem",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_AvatarId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "TodoItem");
        }
    }
}
