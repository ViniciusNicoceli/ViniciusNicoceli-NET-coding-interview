using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Infrastructure.Repositories;

public class BaseRepository<TEntity>(SecureFlightDbContext context)
    : IRepository<TEntity>
    where TEntity : class
{
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public TEntity Update(TEntity entity)
    {
        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
        return entity;
    }

    public async Task<TEntity> GetByIdAsync(params object[] keyValues)
    {
        return await context.Set<TEntity>().FindAsync(keyValues);
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}