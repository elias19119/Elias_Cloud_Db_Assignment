using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;

namespace WidgetCo.Model.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }  
        
        public List<Product> Products { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string Description { get; set; }
    }
}
