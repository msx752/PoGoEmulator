using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PoGoEmulator.Database;

namespace PoGoEmulator.Migrations
{
    [DbContext(typeof(PoGoDbContext))]
    partial class PoGoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PoGoEmulator.Database.Tables.Gym", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cell_id")
                        .HasMaxLength(64);

                    b.Property<bool>("in_battle");

                    b.Property<double>("latitude");

                    b.Property<double>("longitude");

                    b.Property<int>("points");

                    b.Property<byte>("team");

                    b.HasKey("id");

                    b.ToTable("gym");
                });

            modelBuilder.Entity("PoGoEmulator.Database.Tables.OwnedPokemon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("additional_cp_multiplier");

                    b.Property<int>("battles_attacked");

                    b.Property<int>("battles_defended");

                    b.Property<string>("captured_cell_id")
                        .HasMaxLength(32);

                    b.Property<int>("cp");

                    b.Property<double>("cp_multiplier");

                    b.Property<int?>("creation_time_ms");

                    b.Property<long?>("deployed_fort_id");

                    b.Property<int>("dex_number");

                    b.Property<string>("egg_incubator_id");

                    b.Property<double?>("egg_km_walked_start");

                    b.Property<double?>("egg_km_walked_target");

                    b.Property<bool?>("favorite");

                    b.Property<int?>("from_fort");

                    b.Property<double>("height_m");

                    b.Property<int>("individual_attack");

                    b.Property<int>("individual_defense");

                    b.Property<int>("individual_stamina");

                    b.Property<bool>("is_egg");

                    b.Property<string>("move_1")
                        .HasMaxLength(32);

                    b.Property<string>("move_2")
                        .HasMaxLength(32);

                    b.Property<string>("nickname");

                    b.Property<int?>("num_upgrades");

                    b.Property<int?>("origin");

                    b.Property<int>("owner_id");

                    b.Property<string>("pokeball")
                        .HasMaxLength(32);

                    b.Property<int>("stamina");

                    b.Property<int>("stamina_max");

                    b.Property<double>("weight_kg");

                    b.HasKey("id");

                    b.ToTable("owned_pkmn");
                });

            modelBuilder.Entity("PoGoEmulator.Database.Tables.PokeStop", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cell_id")
                        .HasMaxLength(64);

                    b.Property<string>("description")
                        .HasMaxLength(128);

                    b.Property<int>("experience");

                    b.Property<string>("img_url")
                        .HasMaxLength(64);

                    b.Property<double>("latitude");

                    b.Property<double>("longitude");

                    b.Property<string>("name")
                        .HasMaxLength(64);

                    b.Property<string>("rewards")
                        .HasMaxLength(64);

                    b.HasKey("id");

                    b.ToTable("pokestop");
                });

            modelBuilder.Entity("PoGoEmulator.Database.Tables.SpawnPoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cell_id")
                        .HasMaxLength(64);

                    b.Property<string>("encounters")
                        .HasMaxLength(64);

                    b.Property<double>("latitude");

                    b.Property<double>("longitute");

                    b.Property<int>("max_spawn_expire");

                    b.Property<int>("min_spawn_expire");

                    b.Property<int>("update_interval");

                    b.HasKey("id");

                    b.ToTable("spawn_points");
                });

            modelBuilder.Entity("PoGoEmulator.Database.Tables.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("altitude");

                    b.Property<string>("avatar")
                        .HasMaxLength(128);

                    b.Property<string>("candies")
                        .HasMaxLength(1024);

                    b.Property<string>("email")
                        .HasMaxLength(32);

                    b.Property<int>("exp");

                    b.Property<string>("items")
                        .HasMaxLength(255);

                    b.Property<double>("latitude");

                    b.Property<short>("level");

                    b.Property<double>("longitude");

                    b.Property<int>("pokecoins");

                    b.Property<string>("pokedex")
                        .HasMaxLength(64);

                    b.Property<bool>("send_marketing_emails");

                    b.Property<bool>("send_push_notifications");

                    b.Property<int>("stardust");

                    b.Property<byte>("team");

                    b.Property<string>("tutorial")
                        .HasMaxLength(64);

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("users");
                });
        }
    }
}
