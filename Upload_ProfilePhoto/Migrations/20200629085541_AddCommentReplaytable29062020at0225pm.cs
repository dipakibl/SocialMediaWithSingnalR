using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upload_ProfilePhoto.Migrations
{
    public partial class AddCommentReplaytable29062020at0225pm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PictureCommentReplays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    PictureCommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureCommentReplays", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureCommentReplays");
        }
    }
}
