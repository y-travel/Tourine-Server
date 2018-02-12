using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Customers;

namespace Tourine.ServiceInterfaces.Users
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }

        [References(typeof(Customer))]
        public Guid CustomerId { get; set; }
        [Reference]
        public Customer Customer { get; set; }

        public Role Role { get; set; }

        [Ignore]
        public List<Role> Roles { get; set; }
    }
}
