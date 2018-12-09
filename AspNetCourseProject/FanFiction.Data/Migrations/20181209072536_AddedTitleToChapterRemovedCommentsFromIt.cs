using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class AddedTitleToChapterRemovedCommentsFromIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Chapters_ChapterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ChapterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Chapters",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "ChapterId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ChapterId",
                table: "Comments",
                column: "ChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Chapters_ChapterId",
                table: "Comments",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
