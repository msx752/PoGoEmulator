using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoGoEmulator.Migrations
{
    public partial class init_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gym",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cell_id = table.Column<string>(maxLength: 64, nullable: true),
                    in_battle = table.Column<bool>(nullable: false),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    points = table.Column<int>(nullable: false),
                    team = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gym", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "owned_pkmn",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    additional_cp_multiplier = table.Column<double>(nullable: true),
                    battles_attacked = table.Column<int>(nullable: false),
                    battles_defended = table.Column<int>(nullable: false),
                    captured_cell_id = table.Column<string>(maxLength: 32, nullable: true),
                    cp = table.Column<int>(nullable: false),
                    cp_multiplier = table.Column<double>(nullable: false),
                    creation_time_ms = table.Column<int>(nullable: true),
                    deployed_fort_id = table.Column<long>(nullable: true),
                    dex_number = table.Column<int>(nullable: false),
                    egg_incubator_id = table.Column<string>(nullable: true),
                    egg_km_walked_start = table.Column<double>(nullable: true),
                    egg_km_walked_target = table.Column<double>(nullable: true),
                    favorite = table.Column<bool>(nullable: true),
                    from_fort = table.Column<int>(nullable: true),
                    height_m = table.Column<double>(nullable: false),
                    individual_attack = table.Column<int>(nullable: false),
                    individual_defense = table.Column<int>(nullable: false),
                    individual_stamina = table.Column<int>(nullable: false),
                    is_egg = table.Column<bool>(nullable: false),
                    move_1 = table.Column<string>(maxLength: 32, nullable: true),
                    move_2 = table.Column<string>(maxLength: 32, nullable: true),
                    nickname = table.Column<string>(nullable: true),
                    num_upgrades = table.Column<int>(nullable: true),
                    origin = table.Column<int>(nullable: true),
                    owner_id = table.Column<int>(nullable: false),
                    pokeball = table.Column<string>(maxLength: 32, nullable: true),
                    stamina = table.Column<int>(nullable: false),
                    stamina_max = table.Column<int>(nullable: false),
                    weight_kg = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owned_pkmn", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pokestop",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cell_id = table.Column<string>(maxLength: 64, nullable: true),
                    description = table.Column<string>(maxLength: 128, nullable: true),
                    experience = table.Column<int>(nullable: false),
                    img_url = table.Column<string>(maxLength: 64, nullable: true),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: true),
                    rewards = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pokestop", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "spawn_points",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cell_id = table.Column<string>(maxLength: 64, nullable: true),
                    encounters = table.Column<string>(maxLength: 64, nullable: true),
                    latitude = table.Column<double>(nullable: false),
                    longitute = table.Column<double>(nullable: false),
                    max_spawn_expire = table.Column<int>(nullable: false),
                    min_spawn_expire = table.Column<int>(nullable: false),
                    update_interval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spawn_points", x => x.id);
                });

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
                    send_marketing_emails = table.Column<bool>(nullable: false),
                    send_push_notifications = table.Column<bool>(nullable: false),
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
                name: "gym");

            migrationBuilder.DropTable(
                name: "owned_pkmn");

            migrationBuilder.DropTable(
                name: "pokestop");

            migrationBuilder.DropTable(
                name: "spawn_points");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
