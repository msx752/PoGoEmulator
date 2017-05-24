using Microsoft.EntityFrameworkCore;
using PoGoPrivate.EntityFramework.Database.Tables;

namespace PoGoPrivate.EntityFramework.Database
{
    public partial class PoGoContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}