using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class allowingNullableFkInCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FictionStories_FanFictionStoryId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_FanFictionStoryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FanFictionStoryId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FanFictionStoryId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FanFictionStoryId",
                table: "Comments",
                column: "FanFictionStoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FictionStories_FanFictionStoryId",
                table: "Comments",
                column: "FanFictionStoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
