using Microsoft.EntityFrameworkCore.Migrations;

namespace Upload_ProfilePhoto.Migrations
{
    public partial class UpdateNotificaitonTable26042020at1028am : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentedId",
                table: "UserNotifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedId",
                table: "UserNotifications");
        }
    }
}
