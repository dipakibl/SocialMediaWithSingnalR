using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upload_ProfilePhoto.Migrations
{
    public partial class addcommentTable25062020at0100pm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PictureComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureComments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureComments");
        }
    }
}
