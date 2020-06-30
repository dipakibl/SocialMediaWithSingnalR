using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upload_ProfilePhoto.Migrations
{
    public partial class UpdateProfilesPictureGalleryTable30062020at1128am : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProfilesPictureGalleries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "ProfilesPictureGalleries",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ProfilesPictureGalleries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProfilesPictureGalleries");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "ProfilesPictureGalleries");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ProfilesPictureGalleries");
        }
    }
}
