using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class fixingUserStoryMappingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersStories_FictionStories_FanFictionStoryId",
                table: "UsersStories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersStories",
                table: "UsersStories");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "UsersStories");

            migrationBuilder.AlterColumn<int>(
                name: "FanFictionStoryId",
                table: "UsersStories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersStories",
                table: "UsersStories",
                columns: new[] { "FanfictionUserId", "FanFictionStoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStories_FictionStories_FanFictionStoryId",
                table: "UsersStories",
                column: "FanFictionStoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersStories_FictionStories_FanFictionStoryId",
                table: "UsersStories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersStories",
                table: "UsersStories");

            migrationBuilder.AlterColumn<int>(
                name: "FanFictionStoryId",
                table: "UsersStories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "UsersStories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersStories",
                table: "UsersStories",
                columns: new[] { "FanfictionUserId", "StoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStories_FictionStories_FanFictionStoryId",
                table: "UsersStories",
                column: "FanFictionStoryId",
                principalTable: "FictionStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
