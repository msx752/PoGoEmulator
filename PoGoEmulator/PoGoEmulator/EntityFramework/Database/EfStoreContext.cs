using Microsoft.EntityFrameworkCore;
using PoGoEmulator.EntityFramework.Database.Tables;

namespace PoGoEmulator.EntityFramework.Database
{
    public partial class PoGoContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}