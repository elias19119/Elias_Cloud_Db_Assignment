using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Repository.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        public Task<TEntity> GetByIdAsync(TId id);
        public IQueryable<TEntity> GetAllAsync();
        public Task InsertAsync(TEntity entity);
        public void Remove(TEntity entity);
        public Task SaveChanges();
    }
}
