using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Repository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context, context.Users)
        {

        }
    }
}
