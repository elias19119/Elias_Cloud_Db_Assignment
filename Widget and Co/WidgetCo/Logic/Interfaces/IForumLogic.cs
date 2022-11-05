using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widget_and_Co.Model;
using Widget_and_Co.Model.DTO;

namespace Widget_and_Co.Logic.Interfaces
{
    public interface IForumLogic
    {
        public Task<Forum?> GetByIdAsync(Guid id);
        public IQueryable<Forum> GetAllAsync();
        public Task InsertAsync(ForumDTO entity);
        public Task Remove(Guid forumId);
        public Task<Forum> Update(Guid forumId, UpdateForumDTO changes);
    }
}
