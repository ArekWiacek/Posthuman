using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posthuman.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Projects_ProjectId",
                table: "TodoItem");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TodoItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Projects_ProjectId",
                table: "TodoItem",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Projects_ProjectId",
                table: "TodoItem");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TodoItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Projects_ProjectId",
                table: "TodoItem",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
