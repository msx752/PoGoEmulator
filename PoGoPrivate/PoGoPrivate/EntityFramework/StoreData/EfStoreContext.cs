using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PoGoPrivate.EntityFramework.StoreData;

namespace BetterEntityFramework.StoreData
{
    public partial class EfStoreContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}