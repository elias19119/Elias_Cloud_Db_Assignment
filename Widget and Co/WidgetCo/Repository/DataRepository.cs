using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Repository
{
    public abstract class DataRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbset;

        protected DataRepository(DbContext context, DbSet<TEntity> dbset)
        {
            _context = context;
            _dbset = dbset;
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbset.FindAsync(id);
        }

        public IAsyncEnumerable<TEntity> GetAllAsync()
        {
            return _dbset.AsAsyncEnumerable();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        IQueryable<TEntity> IRepository<TEntity, TId>.GetAllAsync()
        {
            return _dbset.AsQueryable();
        }
    }
}
