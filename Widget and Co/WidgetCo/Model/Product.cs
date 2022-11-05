using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public string ProductSpecification { get; set; }

        public string ImageURLs { get; set; }

        public Product(Guid productId, string productName, double price, string productSpecification, string imageURLs)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            ProductSpecification = productSpecification;
            ImageURLs = imageURLs;
        }   

        public Product()
        {

        }
    }
}
