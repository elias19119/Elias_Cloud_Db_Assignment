using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;

namespace Widget_and_Co.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
