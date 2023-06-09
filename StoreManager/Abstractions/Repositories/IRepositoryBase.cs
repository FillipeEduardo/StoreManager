﻿using System.Linq.Expressions;

namespace StoreManager.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        TEntity Delete(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetByFunc(Expression<Func<TEntity, bool>> func);
        TEntity Update(TEntity entity);
        Task Commit();
        Task<List<TEntity>> GetAllWithInclude(string propNav);
        Task<List<TEntity>> GetByFuncWithInclude(string propNav, Expression<Func<TEntity, bool>> func);
        Task<List<TEntity>> GetListByFunc(Expression<Func<TEntity, bool>> func);
    }
}