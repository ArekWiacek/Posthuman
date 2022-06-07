using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration5_HabitsUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInstanceDate",
                table: "Habits");

            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousInstanceDate",
                table: "Habits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousInstanceDate",
                table: "Habits");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInstanceDate",
                table: "Habits",
                type: "datetime2",
                nullable: true);
        }
    }
}
