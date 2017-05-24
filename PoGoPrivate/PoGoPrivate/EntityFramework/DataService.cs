using System;
using Microsoft.EntityFrameworkCore;
using EfStoreContext = PoGoPrivate.EntityFramework.Database.PoGoContext;

/// <summary>
/// https://github.com/hoagsie/BetterEntityFramework 
/// </summary>
namespace PoGoPrivate.EntityFramework
{
    public class DatabaseService : IDisposable
    {
        private EfStoreContext _data;
        private DbContextOptions _lastOptions;

        /// <summary>
        /// Gets an instance of the data service. 
        /// </summary>
        public EfStoreContext Service
        {
            get
            {
                if (_data != null)
                {
                    return _data;
                }

                _lastOptions = _lastOptions ?? new DataOptionsBuilder().Build();
                _data = new EfStoreContext(_lastOptions);

                return _data;
            }
        }

        public DatabaseService()
        {
        }

        public DatabaseService(DataOptionsBuilder builder)
        {
            _lastOptions = builder.Build();
        }

        public DataOptionsBuilder Configure()
        {
            return new DataOptionsBuilder();
        }

        public void UseConfiguration(DataOptionsBuilder builder)
        {
            _data?.Dispose();

            _data = new EfStoreContext(builder.Build());
        }

        public DatabaseService WithScopedService()
        {
            var scopedService = new DatabaseService();
            scopedService.UseConfiguration(new DataOptionsBuilder(_lastOptions));
            return scopedService;
        }

        public DatabaseService WithScopedService(DataOptionsBuilder builder)
        {
            var scopedService = new DatabaseService();
            scopedService.UseConfiguration(builder);
            return scopedService;
        }

        public void Dispose()
        {
            _data?.Dispose();
        }
    }
}