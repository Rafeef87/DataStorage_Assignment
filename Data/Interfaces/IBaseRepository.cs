using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveAsync();
}
