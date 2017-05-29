using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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

                    b.Property<byte>("send_marketing_emails");

                    b.Property<byte>("send_push_notifications");

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
