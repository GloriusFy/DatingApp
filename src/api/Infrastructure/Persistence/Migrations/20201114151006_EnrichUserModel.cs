using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    public partial class EnrichUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "created",
                table: "user",
                type: "timestamp",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<LocalDate>(
                name: "date_of_birth",
                table: "user",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "known_as",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Instant>(
                name: "last_active",
                table: "user",
                type: "timestamp",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    photo_url = table.Column<string>(type: "text", nullable: false),
                    photo_is_main = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo", x => new { x.user_id, x.id });
                    table.ForeignKey(
                        name: "fk_photo_users_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropColumn(
                name: "about",
                table: "user");

            migrationBuilder.DropColumn(
                name: "city",
                table: "user");

            migrationBuilder.DropColumn(
                name: "country",
                table: "user");

            migrationBuilder.DropColumn(
                name: "created",
                table: "user");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "user");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "user");

            migrationBuilder.DropColumn(
                name: "known_as",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_active",
                table: "user");
        }
    }
}
