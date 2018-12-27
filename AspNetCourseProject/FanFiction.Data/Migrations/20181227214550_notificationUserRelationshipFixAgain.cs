using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class notificationUserRelationshipFixAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_FanFictionUserId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_FanFictionUserId",
                table: "Notifications",
                column: "FanFictionUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_FanFictionUserId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_FanFictionUserId",
                table: "Notifications",
                column: "FanFictionUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
