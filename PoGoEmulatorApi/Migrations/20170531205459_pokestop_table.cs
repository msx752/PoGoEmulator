using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoGoEmulatorApi.Migrations
{
    public partial class pokestop_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pokestop");
        }
    }
}
