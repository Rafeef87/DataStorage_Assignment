using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    #region Transaction Management
    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }
    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }
    #endregion

    #region CRUD
    //CREATE
    public virtual async Task AddAsync(TEntity entity)
    {
         await _dbSet.AddAsync(entity);
    }
    //Detta är genererat av Chat GPT 4o - en metod för att inkludera relaterade tabeller. READ
    public virtual async Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }
    //END genererat av Chat GPT 4o.

    //READ
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
       var entities = await _dbSet.ToListAsync();
        return entities;
    }
    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {      
        var entity = await _dbSet.FirstOrDefaultAsync(expression);
        return entity;
    }
    //UPDATE
    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    //DELETE
    public virtual void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
     public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
     {
        return await _dbSet.AnyAsync(expression);
     }
    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
#endregion
}
