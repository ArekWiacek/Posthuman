using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class UsersAsOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "EventItems");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "TodoItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TodoItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EventItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventItems");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "TodoItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "EventItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Avatars_AvatarId",
                table: "TodoItem",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
