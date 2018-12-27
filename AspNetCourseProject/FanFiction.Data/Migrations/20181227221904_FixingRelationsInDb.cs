using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class FixingRelationsInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryRatings_AspNetUsers_UserId",
                table: "StoryRatings");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRatings_AspNetUsers_UserId",
                table: "StoryRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryRatings_AspNetUsers_UserId",
                table: "StoryRatings");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryRatings_AspNetUsers_UserId",
                table: "StoryRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
