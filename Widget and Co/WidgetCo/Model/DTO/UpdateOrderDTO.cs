using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model.DTO
{
    public class UpdateOrderDTO
    {
        public DateTime OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string Description { get; set; }
    }
}
