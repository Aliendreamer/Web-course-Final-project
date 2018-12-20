using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class justCheckingIfModelsAreOk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoryTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Chapters",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoryTypes",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Chapters",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
