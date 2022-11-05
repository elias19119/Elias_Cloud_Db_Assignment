using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;
using Widget_and_Co.Repository;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Logic
{
    public class ProductLogic : IProductLogic
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepsitory;

        public ProductLogic(ILoggerFactory loggerFactory, IProductRepository orderRepository)
        {
            _logger = loggerFactory.CreateLogger<ProductLogic>();
            _productRepsitory = orderRepository;
        }

        public IQueryable<Product> GetAllAsync()
        {
            return  _productRepsitory.GetAllAsync()?? null;
        }

        public  async Task<Product> GetByIdAsync(Guid id)
        {
            return  await _productRepsitory.GetByIdAsync(id) ?? throw new Exception("productId");
        }

        public Task InsertAsync(ProductDTO entity)
        {
            Product product = new Product();
            product.ProductId = Guid.NewGuid();
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.ProductSpecification = entity.ProductSpecification;
            product.ImageURLs = entity.ImageURLs;

            return _productRepsitory.InsertAsync(product)?? null;
        }

        public  async Task Remove(Guid productId)
        {
            Product product = await GetByIdAsync(productId)?? null;
            _productRepsitory.Remove(product);
            await _productRepsitory.SaveChanges();
        }

        public async Task<Product> Update(Guid productId, UpdateProductDTO changes)
        {
            Product product = await _productRepsitory.GetByIdAsync(productId) ?? null;

            product.Price = changes.Price;
            product.ProductName = changes.ProductName;
            product.ImageURLs = changes.ImageURLs;
            product.ProductSpecification = changes.ProductSpecification;

            await _productRepsitory.SaveChanges();
            return product;
        }
    }
}
