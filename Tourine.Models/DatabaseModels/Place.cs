using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Place
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
