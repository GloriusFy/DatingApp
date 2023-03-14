using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class CorrectPhotoTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "photo_url",
                table: "photo",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "photo_is_main",
                table: "photo",
                newName: "is_main");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "photo",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Photo",
                newName: "photo_url");

            migrationBuilder.RenameColumn(
                name: "is_main",
                table: "Photo",
                newName: "photo_is_main");
        }
    }
}
