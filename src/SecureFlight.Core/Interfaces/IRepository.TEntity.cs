using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecureFlight.Core.Interfaces;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> AddAsync(TEntity entity);

    TEntity Update(TEntity entity);

    Task<TEntity> GetByIdAsync(params object[] keyValues);

    Task<int> SaveChangesAsync();
}