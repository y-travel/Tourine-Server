using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [References(typeof(Role))]
        public int RoleId { get; set; }
        [Reference]
        public Role Role { get; set; }
    }
}
