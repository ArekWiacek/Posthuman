using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class TodoItemCycles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instances",
                table: "TodoItemCycles");

            migrationBuilder.AddColumn<int>(
                name: "InstancesToComplete",
                table: "TodoItemCycles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCompletionDate",
                table: "TodoItemCycles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalInstances",
                table: "TodoItemCycles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstancesToComplete",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "LastCompletionDate",
                table: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "TotalInstances",
                table: "TodoItemCycles");

            migrationBuilder.AddColumn<int>(
                name: "Instances",
                table: "TodoItemCycles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
