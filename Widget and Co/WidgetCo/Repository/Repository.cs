using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Repository
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbset;

        protected Repository(DbContext context, DbSet<TEntity> dbset)
        {
            _context = context;
            _dbset = dbset;
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbset.AnyAsync(predicate);
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

        public IQueryable<TEntity> GetAllAsync()
        {
            return _dbset.AsQueryable();
        }
    }
}
