using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class UserAvatarConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Avatars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_UserId",
                table: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_UserId",
                table: "Avatars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Avatars");
        }
    }
}
