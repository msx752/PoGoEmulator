using Microsoft.EntityFrameworkCore;

namespace PoGoPrivate.EntityFramework.Database
{
    public partial class PoGoContext
    {
        private readonly DbContextOptions _options;

        public PoGoContext()
        {
        }

        public PoGoContext(DbContextOptions options) : base(options)
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