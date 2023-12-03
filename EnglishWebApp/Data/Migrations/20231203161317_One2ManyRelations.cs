using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class One2ManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "FillTheGapTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_ThemeId",
                table: "Words",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_FillTheGapTasks_ThemeId",
                table: "FillTheGapTasks",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FillTheGapTasks_Themes_ThemeId",
                table: "FillTheGapTasks",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Themes_ThemeId",
                table: "Words",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FillTheGapTasks_Themes_ThemeId",
                table: "FillTheGapTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Themes_ThemeId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_ThemeId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_FillTheGapTasks_ThemeId",
                table: "FillTheGapTasks");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "FillTheGapTasks");
        }
    }
}
