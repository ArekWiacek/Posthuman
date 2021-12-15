using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RewardCardDiscovery_Avatars_AvatarId",
                table: "RewardCardDiscovery");

            migrationBuilder.DropForeignKey(
                name: "FK_RewardCardDiscovery_RewardCards_RewardCardId",
                table: "RewardCardDiscovery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RewardCardDiscovery",
                table: "RewardCardDiscovery");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RewardCards");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "RewardCards");

            migrationBuilder.DropColumn(
                name: "DiscoveryTime",
                table: "RewardCardDiscovery");

            migrationBuilder.RenameTable(
                name: "RewardCardDiscovery",
                newName: "RewardCardsDiscoveries");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardDiscovery_RewardCardId",
                table: "RewardCardsDiscoveries",
                newName: "IX_RewardCardsDiscoveries_RewardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardDiscovery_AvatarId",
                table: "RewardCardsDiscoveries",
                newName: "IX_RewardCardsDiscoveries_AvatarId");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "RewardCards",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body2",
                table: "RewardCards",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscoveryDate",
                table: "RewardCardsDiscoveries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RewardCardsDiscoveries",
                table: "RewardCardsDiscoveries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RewardCardsDiscoveries_Avatars_AvatarId",
                table: "RewardCardsDiscoveries",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RewardCardsDiscoveries_RewardCards_RewardCardId",
                table: "RewardCardsDiscoveries",
                column: "RewardCardId",
                principalTable: "RewardCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RewardCardsDiscoveries_Avatars_AvatarId",
                table: "RewardCardsDiscoveries");

            migrationBuilder.DropForeignKey(
                name: "FK_RewardCardsDiscoveries_RewardCards_RewardCardId",
                table: "RewardCardsDiscoveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RewardCardsDiscoveries",
                table: "RewardCardsDiscoveries");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "RewardCards");

            migrationBuilder.DropColumn(
                name: "Body2",
                table: "RewardCards");

            migrationBuilder.DropColumn(
                name: "DiscoveryDate",
                table: "RewardCardsDiscoveries");

            migrationBuilder.RenameTable(
                name: "RewardCardsDiscoveries",
                newName: "RewardCardDiscovery");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardsDiscoveries_RewardCardId",
                table: "RewardCardDiscovery",
                newName: "IX_RewardCardDiscovery_RewardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardsDiscoveries_AvatarId",
                table: "RewardCardDiscovery",
                newName: "IX_RewardCardDiscovery_AvatarId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RewardCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "RewardCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DiscoveryTime",
                table: "RewardCardDiscovery",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RewardCardDiscovery",
                table: "RewardCardDiscovery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RewardCardDiscovery_Avatars_AvatarId",
                table: "RewardCardDiscovery",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RewardCardDiscovery_RewardCards_RewardCardId",
                table: "RewardCardDiscovery",
                column: "RewardCardId",
                principalTable: "RewardCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
