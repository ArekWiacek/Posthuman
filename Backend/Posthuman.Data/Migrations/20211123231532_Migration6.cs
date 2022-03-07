using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedInstances",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Instances",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstancesStreak",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissedInstances",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepetitionPeriod",
                table: "TodoItem",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TodoItem",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RewardCardDiscovery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(nullable: false),
                    DiscoveryTime = table.Column<DateTime>(nullable: false),
                    RewardCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardCardDiscovery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RewardCardDiscovery_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RewardCardDiscovery_RewardCards_RewardCardId",
                        column: x => x.RewardCardId,
                        principalTable: "RewardCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RewardCardDiscovery_AvatarId",
                table: "RewardCardDiscovery",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardCardDiscovery_RewardCardId",
                table: "RewardCardDiscovery",
                column: "RewardCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RewardCardDiscovery");

            migrationBuilder.DropColumn(
                name: "CompletedInstances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "Instances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "InstancesStreak",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "MissedInstances",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "RepetitionPeriod",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TodoItem");
        }
    }
}
