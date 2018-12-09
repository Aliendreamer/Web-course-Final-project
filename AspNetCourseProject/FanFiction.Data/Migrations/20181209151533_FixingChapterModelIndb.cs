using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class FixingChapterModelIndb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId1",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_FanFictionStoryId1",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "FanFictionStoryId1",
                table: "Chapters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FanFictionStoryId1",
                table: "Chapters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_FanFictionStoryId1",
                table: "Chapters",
                column: "FanFictionStoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId1",
                table: "Chapters",
                column: "FanFictionStoryId1",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
