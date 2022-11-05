using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Repository
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context, context.Orders)
        {
        }
    }
}
