using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Reference]
        public Role Role { get; set; }
    }
}
