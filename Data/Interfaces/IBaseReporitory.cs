﻿
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseReporitory<TEntity> where TEntity : class
{
    Task<bool> AlreadyExisitAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updateEntity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

}
