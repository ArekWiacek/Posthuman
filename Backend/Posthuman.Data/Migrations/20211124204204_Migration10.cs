using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_RewardCards",
                table: "RewardCards");

            migrationBuilder.RenameTable(
                name: "RewardCardsDiscoveries",
                newName: "TechnologyCardsDiscoveries");

            migrationBuilder.RenameTable(
                name: "RewardCards",
                newName: "TechnologyCards");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardsDiscoveries_RewardCardId",
                table: "TechnologyCardsDiscoveries",
                newName: "IX_TechnologyCardsDiscoveries_RewardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_RewardCardsDiscoveries_AvatarId",
                table: "TechnologyCardsDiscoveries",
                newName: "IX_TechnologyCardsDiscoveries_AvatarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnologyCardsDiscoveries",
                table: "TechnologyCardsDiscoveries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnologyCards",
                table: "TechnologyCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyCardsDiscoveries_Avatars_AvatarId",
                table: "TechnologyCardsDiscoveries",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyCardsDiscoveries_TechnologyCards_RewardCardId",
                table: "TechnologyCardsDiscoveries",
                column: "RewardCardId",
                principalTable: "TechnologyCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyCardsDiscoveries_Avatars_AvatarId",
                table: "TechnologyCardsDiscoveries");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnologyCardsDiscoveries_TechnologyCards_RewardCardId",
                table: "TechnologyCardsDiscoveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnologyCardsDiscoveries",
                table: "TechnologyCardsDiscoveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnologyCards",
                table: "TechnologyCards");

            migrationBuilder.RenameTable(
                name: "TechnologyCardsDiscoveries",
                newName: "RewardCardsDiscoveries");

            migrationBuilder.RenameTable(
                name: "TechnologyCards",
                newName: "RewardCards");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyCardsDiscoveries_RewardCardId",
                table: "RewardCardsDiscoveries",
                newName: "IX_RewardCardsDiscoveries_RewardCardId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnologyCardsDiscoveries_AvatarId",
                table: "RewardCardsDiscoveries",
                newName: "IX_RewardCardsDiscoveries_AvatarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RewardCardsDiscoveries",
                table: "RewardCardsDiscoveries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RewardCards",
                table: "RewardCards",
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
    }
}
