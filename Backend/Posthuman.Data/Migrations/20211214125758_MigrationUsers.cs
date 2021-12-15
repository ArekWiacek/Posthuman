using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class MigrationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnologyCardDiscoveryId",
                table: "Requirements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_TechnologyCardDiscoveryId",
                table: "Requirements",
                column: "TechnologyCardDiscoveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_TechnologyCardsDiscoveries_TechnologyCardDiscoveryId",
                table: "Requirements",
                column: "TechnologyCardDiscoveryId",
                principalTable: "TechnologyCardsDiscoveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_TechnologyCardsDiscoveries_TechnologyCardDiscoveryId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_TechnologyCardDiscoveryId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "TechnologyCardDiscoveryId",
                table: "Requirements");
        }
    }
}
