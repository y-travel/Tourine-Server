using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Destination
    {
        [AutoIncrement]
        public Guid Id { get; set; }
        public string Name { get; set; }    
    }
}
