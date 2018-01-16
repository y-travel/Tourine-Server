using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Destination
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }    
    }
}
