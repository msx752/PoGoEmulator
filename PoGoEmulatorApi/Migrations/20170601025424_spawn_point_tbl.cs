using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoGoEmulatorApi.Migrations
{
    public partial class spawn_point_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spawn_points");
        }
    }
}
