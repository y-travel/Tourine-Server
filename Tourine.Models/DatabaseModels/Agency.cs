using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Agency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
