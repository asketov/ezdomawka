using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class initFavorThemeSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_FavorSolutions_FavorSolutionId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FavorSolutionId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FavorSolutionId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "FavorSolutions");

            migrationBuilder.AddColumn<string>(
                name: "Connection",
                table: "FavorSolutions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FavorSubject",
                columns: table => new
                {
                    FavorSolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavorSubject", x => new { x.FavorSolutionId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_FavorSubject_FavorSolutions_FavorSolutionId",
                        column: x => x.FavorSolutionId,
                        principalTable: "FavorSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavorSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavorTheme",
                columns: table => new
                {
                    FavorSolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThemeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavorTheme", x => new { x.FavorSolutionId, x.ThemeId });
                    table.ForeignKey(
                        name: "FK_FavorTheme_FavorSolutions_FavorSolutionId",
                        column: x => x.FavorSolutionId,
                        principalTable: "FavorSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavorTheme_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavorSubject_SubjectId",
                table: "FavorSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FavorTheme_ThemeId",
                table: "FavorTheme",
                column: "ThemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavorSubject");

            migrationBuilder.DropTable(
                name: "FavorTheme");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropColumn(
                name: "Connection",
                table: "FavorSolutions");

            migrationBuilder.AddColumn<Guid>(
                name: "FavorSolutionId",
                table: "Subjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "FavorSolutions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FavorSolutionId",
                table: "Subjects",
                column: "FavorSolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_FavorSolutions_FavorSolutionId",
                table: "Subjects",
                column: "FavorSolutionId",
                principalTable: "FavorSolutions",
                principalColumn: "Id");
        }
    }
}
