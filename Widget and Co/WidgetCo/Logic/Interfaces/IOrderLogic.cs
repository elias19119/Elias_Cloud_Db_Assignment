using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;

namespace Widget_and_Co.Logic.Interfaces
{
    public interface IOrderLogic
    {
        public Task<Order?> GetByIdAsync(Guid id);
        public IQueryable<Order> GetAllAsync();
        public Task InsertAsync(OrderDTO entity);
        public Task Remove(Guid orderId);
        public Task<Order> Update(Guid orderId, UpdateOrderDTO changes);
        Task<Order> UpdateShippingDate(Guid orderId);

    }
}
