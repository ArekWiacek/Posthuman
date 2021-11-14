using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Bio = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false),
                    ExpToNewLevel = table.Column<int>(nullable: false),
                    CybertribeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Occured = table.Column<DateTime>(nullable: false),
                    RelatedEntityType = table.Column<string>(maxLength: 20, nullable: true),
                    RelatedEntityId = table.Column<int>(nullable: true),
                    ExpGained = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    AvatarId = table.Column<int>(nullable: false),
                    TotalSubtasks = table.Column<int>(nullable: false),
                    CompletedSubtasks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    AvatarId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItem_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoItem_TodoItem_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TodoItem_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AvatarId",
                table: "Projects",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_AvatarId",
                table: "TodoItem",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_ParentId",
                table: "TodoItem",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_ProjectId",
                table: "TodoItem",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventItems");

            migrationBuilder.DropTable(
                name: "TodoItem");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Avatars");
        }
    }
}
