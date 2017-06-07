using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PoGoEmulator.Database.Tables;

namespace PoGoEmulator.Database
{
    public class PoGoDbContext : DbContext
    {
        private readonly DbContextOptions _options = null;

        public DbSet<User> Users { get; set; }
        public DbSet<OwnedPokemon> OwnedPokemons { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<PokeStop> PokeStops { get; set; }
        public DbSet<SpawnPoint> SpawnPoints { get; set; }

        public PoGoDbContext() : base()
        {
        }

        public PoGoDbContext(DbContextOptions<PoGoDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer(GlobalSettings.ServerCfg.SqlConnectionString);//setactivemultiple=true whether necessary or not ?
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}