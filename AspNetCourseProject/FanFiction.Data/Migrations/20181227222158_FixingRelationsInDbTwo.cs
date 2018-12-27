using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class FixingRelationsInDbTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FictionStories_AspNetUsers_AuthorId",
                table: "FictionStories");

            migrationBuilder.AddForeignKey(
                name: "FK_FictionStories_AspNetUsers_AuthorId",
                table: "FictionStories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FictionStories_AspNetUsers_AuthorId",
                table: "FictionStories");

            migrationBuilder.AddForeignKey(
                name: "FK_FictionStories_AspNetUsers_AuthorId",
                table: "FictionStories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
