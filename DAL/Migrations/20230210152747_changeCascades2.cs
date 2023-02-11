using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeCascades2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavorSolutions_Themes_ThemeId",
                table: "FavorSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubject_Subjects_SubjectId",
                table: "FavorSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSolutions_Themes_ThemeId",
                table: "FavorSolutions",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubject_Subjects_SubjectId",
                table: "FavorSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavorSolutions_Themes_ThemeId",
                table: "FavorSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubject_Subjects_SubjectId",
                table: "FavorSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSolutions_Themes_ThemeId",
                table: "FavorSolutions",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubject_Subjects_SubjectId",
                table: "FavorSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
