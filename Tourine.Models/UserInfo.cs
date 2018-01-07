using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Reference]
        public Role Role { get; set; }
    }
}
