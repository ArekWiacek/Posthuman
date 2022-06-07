using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Posthuman.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Subtitle = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    AdditionalText = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologyCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Subtitle = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    Body2 = table.Column<string>(nullable: false),
                    RequiredLevel = table.Column<int>(nullable: false),
                    Categories = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Bio = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false),
                    ExpToNewLevel = table.Column<int>(nullable: false),
                    ExpToCurrentLevel = table.Column<int>(nullable: false),
                    CybertribeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "TechnologyCardsDiscoveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(nullable: false),
                    HasBeenSeen = table.Column<bool>(nullable: false),
                    DiscoveryDate = table.Column<DateTime>(nullable: false),
                    RewardCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyCardsDiscoveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnologyCardsDiscoveries_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnologyCardsDiscoveries_TechnologyCards_RewardCardId",
                        column: x => x.RewardCardId,
                        principalTable: "TechnologyCards",
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
                    IsVisible = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    AvatarId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    IsCyclic = table.Column<bool>(nullable: false),
                    TodoItemCycleId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 20, nullable: false),
                    Level = table.Column<int>(nullable: true),
                    Exp = table.Column<int>(nullable: true),
                    TechnologyCardDiscoveryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_TechnologyCardsDiscoveries_TechnologyCardDiscoveryId",
                        column: x => x.TechnologyCardDiscoveryId,
                        principalTable: "TechnologyCardsDiscoveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoItemCycles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoItemId = table.Column<int>(nullable: false),
                    IsInfinite = table.Column<bool>(nullable: false),
                    RepetitionPeriod = table.Column<string>(maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    NextIstanceDate = table.Column<DateTime>(nullable: false),
                    LastInstanceDate = table.Column<DateTime>(nullable: true),
                    LastCompletedInstanceDate = table.Column<DateTime>(nullable: true),
                    TotalInstances = table.Column<int>(nullable: false),
                    CompletedInstances = table.Column<int>(nullable: false),
                    MissedInstances = table.Column<int>(nullable: false),
                    InstancesToComplete = table.Column<int>(nullable: false),
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
                name: "IX_Avatars_UserId",
                table: "Avatars",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AvatarId",
                table: "Projects",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_TechnologyCardDiscoveryId",
                table: "Requirements",
                column: "TechnologyCardDiscoveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyCardsDiscoveries_AvatarId",
                table: "TechnologyCardsDiscoveries",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnologyCardsDiscoveries_RewardCardId",
                table: "TechnologyCardsDiscoveries",
                column: "RewardCardId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TodoItemCycles_TodoItemId",
                table: "TodoItemCycles",
                column: "TodoItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "EventItems");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "TodoItemCycles");

            migrationBuilder.DropTable(
                name: "TechnologyCardsDiscoveries");

            migrationBuilder.DropTable(
                name: "TodoItem");

            migrationBuilder.DropTable(
                name: "TechnologyCards");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
