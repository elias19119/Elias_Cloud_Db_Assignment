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
    public class ForumLogic : IForumLogic
    {

        private readonly ILogger _logger;
        private readonly IForumRepository _forumRepsitory;

        public ForumLogic(ILoggerFactory loggerFactory, IForumRepository forumRepsitory)
        {
            _logger = loggerFactory.CreateLogger<ForumLogic>();
            _forumRepsitory = forumRepsitory;
        }

        public IQueryable<Forum> GetAllAsync()
        {
            return _forumRepsitory.GetAllAsync() ?? null;
        }

        public Task<Forum> GetByIdAsync(Guid id)
        {
            return _forumRepsitory.GetByIdAsync(id) ?? throw new Exception("Forum Id");
        }

        public Task InsertAsync(ForumDTO entity)
        {
            Forum forum = new Forum();
            forum.ForumId = Guid.NewGuid();
            forum.Comment = entity.Comment;
            forum.Product = entity.Product;

            return _forumRepsitory.InsertAsync(forum) ?? throw new Exception("Forum");
        }

        public async Task Remove(Guid forumId)
        {
            Forum forum = await GetByIdAsync(forumId)?? null;
            _forumRepsitory.Remove(forum);
            await _forumRepsitory.SaveChanges();
        }

        public async Task<Forum> Update(Guid forumId, UpdateForumDTO changes)
        {
            Forum forum = await GetByIdAsync(forumId)?? null;

            forum.Comment = changes.Comment;
            forum.Product = changes.Product;   

            await _forumRepsitory.SaveChanges();
            return forum;
        }
    }
}
