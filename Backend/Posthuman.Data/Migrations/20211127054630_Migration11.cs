using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepetitionInfo");

            migrationBuilder.AddColumn<int>(
                name: "CycleInfoId",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCyclic",
                table: "TodoItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TodoItemCycles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoItemId = table.Column<int>(nullable: false),
                    RepetitionPeriod = table.Column<string>(maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Instances = table.Column<int>(nullable: false),
                    CompletedInstances = table.Column<int>(nullable: false),
                    MissedInstances = table.Column<int>(nullable: false),
                    InstancesStreak = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItemCycles_TodoItem_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemCycles_TodoItemId",
                table: "TodoItemCycles",
                column: "TodoItemId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemCycles");

            migrationBuilder.DropColumn(
                name: "CycleInfoId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "IsCyclic",
                table: "TodoItem");

            migrationBuilder.CreateTable(
                name: "RepetitionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletedInstances = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instances = table.Column<int>(type: "int", nullable: false),
                    InstancesStreak = table.Column<int>(type: "int", nullable: false),
                    MissedInstances = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    RepetitionPeriod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TodoItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepetitionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepetitionInfo_TodoItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepetitionInfo_TodoItem_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepetitionInfo_ParentId",
                table: "RepetitionInfo",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RepetitionInfo_TodoItemId",
                table: "RepetitionInfo",
                column: "TodoItemId");
        }
    }
}
