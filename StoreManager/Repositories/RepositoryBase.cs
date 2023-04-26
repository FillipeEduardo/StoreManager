﻿using Microsoft.EntityFrameworkCore;
using StoreManager.Abstractions.Repositories;
using StoreManager.Data;
using System.Linq.Expressions;

namespace StoreManager.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByFunc(Expression<Func<TEntity, bool>> func)
        {
            return await _dbSet.FirstOrDefaultAsync(func);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
