using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class fixingFanFictionUserConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_AspNetUsers_FanfictionUserId",
                table: "BlockedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_AspNetUsers_FanfictionUserId",
                table: "BlockedUsers",
                column: "FanfictionUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_AspNetUsers_FanfictionUserId",
                table: "BlockedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_AspNetUsers_FanfictionUserId",
                table: "BlockedUsers",
                column: "FanfictionUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
