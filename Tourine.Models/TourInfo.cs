using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    [Alias("Tour")]
    public class TourInfo
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public long Price { get; set; }
        public string Destination { get; set; }
    }
}