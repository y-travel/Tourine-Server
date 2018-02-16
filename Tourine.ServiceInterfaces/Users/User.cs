using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Users
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }

        public Role Role { get; set; }

        [Ignore]
        public List<Role> Roles { get; set; }
    }
}
