using Microsoft.EntityFrameworkCore;

namespace PoGoPrivate.EntityFramework.Database
{
    public partial class EfStoreContext
    {
        private readonly DbContextOptions _options;

        public EfStoreContext()
        {
        }

        public EfStoreContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=pogodb;Trusted_Connection=True;");
            }
        }
    }
}