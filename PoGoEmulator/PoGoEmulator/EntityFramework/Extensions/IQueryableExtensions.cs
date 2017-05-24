using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace PoGoEmulator.EntityFramework.Extensions
{
    internal static class IQueryableExtensions
    {
        public static async Task Insert<TEntity>(this DbSet<TEntity> query, TEntity data, CancellationToken ct = default(CancellationToken))
              where TEntity : class
        {
            await query.AddAsync(data, ct);
        }

        public static async Task UpdateWhere<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> update, CancellationToken ct = default(CancellationToken))
            where TEntity : class
        {
            await query.Where(where).UpdateAsync(update, ct);
        }

        public static async Task DeleteWhere<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> where, CancellationToken ct = default(CancellationToken))
            where TEntity : class
        {
            await query.Where(where).DeleteAsync(ct);
        }
    }
}