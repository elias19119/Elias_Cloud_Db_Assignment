using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User(Guid userId, string firstName, string lastName, string address, string email, ICollection<Order> orders)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Orders = orders;
        }   

        public User()
        {

        }
    }
}
