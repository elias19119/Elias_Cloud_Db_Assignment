using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;


namespace Widget_and_Co.Logic.Interfaces
{
    public interface IProductLogic
    {
        public Task<Product?> GetByIdAsync(Guid id);
        public IQueryable<Product> GetAllAsync();
        public Task InsertAsync(ProductDTO entity);
        public Task Remove(Guid id);
        public Task<Product> Update(Guid productId, UpdateProductDTO changes);
    }
}
