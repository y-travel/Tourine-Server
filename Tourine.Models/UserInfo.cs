using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    [Alias("User")]
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        [Reference]
        public string RoleName { get; set; }
    }
}
