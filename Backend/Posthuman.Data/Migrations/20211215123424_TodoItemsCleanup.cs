using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class TodoItemsCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCompletionDate",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "CycleInfoId",
                table: "TodoItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCompletedInstanceDate",
                table: "TodoItemCycles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInstanceDate",
                table: "TodoItemCycles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextIstanceDate",
                table: "TodoItemCycles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "TodoItem",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodoItemCycleId",
                table: "TodoItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCompletedInstanceDate",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "LastInstanceDate",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "NextIstanceDate",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "TodoItemCycleId",
                table: "TodoItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCompletionDate",
                table: "TodoItemCycles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "TodoItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "CycleInfoId",
                table: "TodoItem",
                type: "int",
                nullable: true);
        }
    }
}
