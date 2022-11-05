using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model.DTO;
using Widget_and_Co.Model;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Repository;
using Microsoft.Extensions.Logging;
using Widget_and_Co.Repository.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Widget_and_Co.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _orderRepsitory;

        public OrderLogic(ILoggerFactory loggerFactory, IOrderRepository orderRepository)
        {
            _logger = loggerFactory.CreateLogger<OrderLogic>();
            _orderRepsitory = orderRepository;
        }

        public IQueryable<Order> GetAllAsync()
        {
            return _orderRepsitory.GetAllAsync().Include(x =>x.Products) ?? null;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _orderRepsitory.GetByIdAsync(id) ?? throw new Exception("order Id");
        }

        public Task InsertAsync(OrderDTO entity)
        {
            Order order = new Order();
            order.OrderId = Guid.NewGuid();
            order.OrderDate = entity.OrderDate;
            order.ShippingDate = entity.ShippingDate;
            order.Description = entity.Description;
            order.Products = entity.Products;

            return _orderRepsitory.InsertAsync(order)?? throw new Exception("Order");
        }

        public async Task Remove(Guid orderId)
        {
            Order order = await GetByIdAsync(orderId)?? null;
            _orderRepsitory.Remove(order);
            await _orderRepsitory.SaveChanges();
        }

        public async Task<Order> Update(Guid orderId, UpdateOrderDTO changes)
        {
            Order order = await _orderRepsitory.GetByIdAsync(orderId)?? null;

            order.OrderDate = changes.OrderDate;
            order.ShippingDate = changes.ShippingDate;
            order.Description = changes.Description;

            await _orderRepsitory.SaveChanges();
            return order;
        }

        public async Task<Order> UpdateShippingDate(Guid orderId)
        {
            Order updateorderShippingTime = await GetByIdAsync(orderId)?? null;

            updateorderShippingTime.ShippingDate = DateTime.Today.AddDays(3);

            await _orderRepsitory.SaveChanges();
            return updateorderShippingTime;

        }
    }
}
