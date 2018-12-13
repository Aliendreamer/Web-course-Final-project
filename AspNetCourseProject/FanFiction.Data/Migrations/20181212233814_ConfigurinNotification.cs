using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFiction.Data.Migrations
{
    public partial class ConfigurinNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedStoryId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedStoryId",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
