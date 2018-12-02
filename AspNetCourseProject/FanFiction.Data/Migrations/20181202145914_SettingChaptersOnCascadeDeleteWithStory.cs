using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class SettingChaptersOnCascadeDeleteWithStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId",
                table: "Chapters",
                column: "FanFictionStoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_FictionStories_FanFictionStoryId",
                table: "Chapters",
                column: "FanFictionStoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
