using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class ChangingStoryCommentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FictionStories_StoryId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FictionStories_StoryId",
                table: "Comments",
                column: "StoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FictionStories_StoryId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FictionStories_StoryId",
                table: "Comments",
                column: "StoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
