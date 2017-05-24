using Microsoft.EntityFrameworkCore;
using PoGoEmulator.Database.Tables;

namespace PoGoEmulator.Database
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
                optionsBuilder.UseSqlServer(Global.Cfg.SqlConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}