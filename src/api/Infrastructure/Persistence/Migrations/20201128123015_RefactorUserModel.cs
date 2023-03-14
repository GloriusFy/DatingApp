using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace Infrastructure.Persistence.Migrations
{
    public partial class RefactorUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "known_as",
                table: "user");

            migrationBuilder.AlterColumn<LocalDate>(
                name: "date_of_birth",
                table: "user",
                type: "date",
                nullable: false,
                defaultValue: new NodaTime.LocalDate(1, 1, 1),
                oldClrType: typeof(LocalDate),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "user");

            migrationBuilder.AlterColumn<LocalDate>(
                name: "date_of_birth",
                table: "user",
                type: "date",
                nullable: true,
                oldClrType: typeof(LocalDate),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "known_as",
                table: "user",
                type: "text",
                nullable: true);
        }
    }
}
