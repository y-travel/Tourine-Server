using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Place
    {
        [AutoIncrement]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
