using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepsitory;

        public UserLogic(ILoggerFactory loggerFactory, IUserRepository userRepsitory)
        {
            _logger = loggerFactory.CreateLogger<UserLogic>();
            _userRepsitory = userRepsitory;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepsitory.GetByIdAsync(id)?? throw new Exception("userId");
        }

        public  Task InsertAsync(UserDTO entity)
        {
            User user = new User();
            user.UserId = Guid.NewGuid();
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Address = entity.Address;
            user.Email = entity.Email;

           _userRepsitory.InsertAsync(user);
           return _userRepsitory.SaveChanges();
        }

        public async Task Remove(Guid userId)
        {
            User user = await GetByIdAsync(userId) ?? null;
            _userRepsitory.Remove(user);
            await _userRepsitory.SaveChanges();
        }

        public async Task<User> Update(Guid userId, UpdateUserDTO changes)
        {
            User user = await GetByIdAsync(userId) ?? null;

            user.FirstName = changes.FirstName;
            user.LastName = changes.LastName;
            user.Address = changes.Address;
            user.Email = changes.Email;

            await _userRepsitory.SaveChanges();
            return user;
        }

        public IQueryable<User> GetAllAsync()
        {
            return _userRepsitory.GetAllAsync().Include(x =>x.Orders)?? null;
        }
    }
}
