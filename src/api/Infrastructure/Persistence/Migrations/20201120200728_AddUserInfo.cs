using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "about",
                table: "user",
                newName: "looking_for");

            migrationBuilder.AddColumn<string>(
                name: "brief_description",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "interests",
                table: "user",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brief_description",
                table: "user");

            migrationBuilder.DropColumn(
                name: "interests",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "looking_for",
                table: "user",
                newName: "about");
        }
    }
}
