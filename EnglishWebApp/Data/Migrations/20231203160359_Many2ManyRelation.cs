using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Many2ManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FillTheGapTaskWord",
                columns: table => new
                {
                    FillTasksId = table.Column<int>(type: "int", nullable: false),
                    WordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FillTheGapTaskWord", x => new { x.FillTasksId, x.WordsId });
                    table.ForeignKey(
                        name: "FK_FillTheGapTaskWord_FillTheGapTasks_FillTasksId",
                        column: x => x.FillTasksId,
                        principalTable: "FillTheGapTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FillTheGapTaskWord_Words_WordsId",
                        column: x => x.WordsId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FillTheGapTaskWord_WordsId",
                table: "FillTheGapTaskWord",
                column: "WordsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FillTheGapTaskWord");
        }
    }
}
