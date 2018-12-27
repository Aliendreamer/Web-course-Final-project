using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class FixingRelationsInDbThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_AspNetUsers_AuthorId",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_AspNetUsers_AuthorId",
                table: "Chapters",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_AspNetUsers_AuthorId",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_AspNetUsers_AuthorId",
                table: "Chapters",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
