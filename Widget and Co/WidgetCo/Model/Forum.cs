using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model
{
    public class Forum
    {
        public Guid ForumId { get; set; }

        public Product Product { get; set; }

        public User? User { get; set; }

        public string Comment { get; set; }
        
        public Forum(Guid forumId, Product product, User user , string comment)
        {
            ForumId = forumId;
            Product = product;
            User = user;
            Comment = comment;
        }

        public Forum()
        {

        }
    }
}
