using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecureFlight.Core.Interfaces;

public interface IService<TEntity>
    where TEntity : class
{
    Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync();

    Task<OperationResult<IReadOnlyList<TEntity>>> FilterAsync(Expression<Func<TEntity, bool>> predicate);
    
    Task<OperationResult<TEntity>> FindAsync(params object[] keyValues);
}