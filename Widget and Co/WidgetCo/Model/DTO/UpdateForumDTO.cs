using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model.DTO
{
    public class UpdateForumDTO
    {
        public Product Product { get; set; }

        public User? User { get; set; }

        public string Comment { get; set; }
    }
}
