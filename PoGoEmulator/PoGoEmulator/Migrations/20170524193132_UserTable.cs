using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoGoEmulator.Migrations
{
    public partial class UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    altitude = table.Column<double>(nullable: false),
                    avatar = table.Column<string>(maxLength: 128, nullable: true),
                    candies = table.Column<string>(maxLength: 1024, nullable: true),
                    email = table.Column<string>(maxLength: 32, nullable: true),
                    exp = table.Column<int>(nullable: false),
                    items = table.Column<string>(maxLength: 255, nullable: true),
                    latitude = table.Column<double>(nullable: false),
                    level = table.Column<short>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    pokecoins = table.Column<int>(nullable: false),
                    pokedex = table.Column<string>(maxLength: 64, nullable: true),
                    send_marketing_emails = table.Column<byte>(nullable: false),
                    send_push_notifications = table.Column<byte>(nullable: false),
                    stardust = table.Column<int>(nullable: false),
                    team = table.Column<byte>(nullable: false),
                    tutorial = table.Column<string>(maxLength: 64, nullable: true),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
