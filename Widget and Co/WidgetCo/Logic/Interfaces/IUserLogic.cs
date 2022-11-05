using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;

namespace Widget_and_Co.Logic.Interfaces
{
    public interface IUserLogic
    {
        public Task<User?> GetByIdAsync(Guid id);
        public IQueryable<User> GetAllAsync();
        public Task InsertAsync(UserDTO entity);
        public Task Remove(Guid userId);
        public Task<User> Update(Guid userId, UpdateUserDTO changes);
    }
}
