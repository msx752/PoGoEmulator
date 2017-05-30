using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PoGoEmulatorApi.Database.Tables;

namespace PoGoEmulatorApi.Database
{
    public class PoGoDbContext : DbContext
    {
        private readonly DbContextOptions _options = null;
        public DbSet<User> Users { get; set; }

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
                optionsBuilder.UseSqlServer(GlobalSettings.Cfg.SqlConnectionString);//setactivemultiple=true whether necessary or not ?
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}