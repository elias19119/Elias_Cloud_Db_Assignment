using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model
{
    public class Order
    {

        public Guid OrderId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string Description { get; set; }

        public Order(Guid orderId, ICollection<Product> products, User user, DateTime orderDate, DateTime? shippingDate, string description)
        {
            OrderId = orderId;
            Products= products;
            User = user;
            OrderDate = orderDate;
            ShippingDate = shippingDate;
            Description = description;
        }
        
        public Order()
        {

        }
    }
}
