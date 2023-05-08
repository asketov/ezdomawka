using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixfks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavorSolutions_Users_AuthorId",
                table: "FavorSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubjects_FavorSolutions_FavorSolutionId",
                table: "FavorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubjects_Subjects_SubjectId",
                table: "FavorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Institutes_InstituteId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Institutes_InstituteId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSolutions_Users_AuthorId",
                table: "FavorSolutions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubjects_FavorSolutions_FavorSolutionId",
                table: "FavorSubjects",
                column: "FavorSolutionId",
                principalTable: "FavorSolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubjects_Subjects_SubjectId",
                table: "FavorSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Institutes_InstituteId",
                table: "Subjects",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Institutes_InstituteId",
                table: "Themes",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavorSolutions_Users_AuthorId",
                table: "FavorSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubjects_FavorSolutions_FavorSolutionId",
                table: "FavorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_FavorSubjects_Subjects_SubjectId",
                table: "FavorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Institutes_InstituteId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Institutes_InstituteId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSolutions_Users_AuthorId",
                table: "FavorSolutions",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubjects_FavorSolutions_FavorSolutionId",
                table: "FavorSubjects",
                column: "FavorSolutionId",
                principalTable: "FavorSolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavorSubjects_Subjects_SubjectId",
                table: "FavorSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Institutes_InstituteId",
                table: "Subjects",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Institutes_InstituteId",
                table: "Themes",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
