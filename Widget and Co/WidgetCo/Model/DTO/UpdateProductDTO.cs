using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model.DTO
{
    public class UpdateProductDTO
    {
        public string ProductName { get; set; }

        public double Price { get; set; }

        public string ProductSpecification { get; set; }

        public string ImageURLs { get; set; }
    }
}
