using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class removeUniqueModifierFromTitleInFictionStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FictionStories_Title",
                table: "FictionStories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FictionStories_Title",
                table: "FictionStories",
                column: "Title",
                unique: true);
        }
    }
}
