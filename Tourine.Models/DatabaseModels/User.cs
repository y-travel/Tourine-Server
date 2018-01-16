using System;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;

namespace Tourine.Models.DatabaseModels
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string Username { get; set; }
        public string Password { get; set; }

        [References(typeof(Customer))]
        public Guid CustomerId { get; set; }
        [Reference]
        public Customer Customer { get; set; }

        public Role Role { get; set; }
    }
}
