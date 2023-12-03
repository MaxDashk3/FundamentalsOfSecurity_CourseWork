using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FillTheGapTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FillTheGapTasks");
        }
    }
}
