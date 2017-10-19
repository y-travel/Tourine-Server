using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    [Alias("Tour")]
    public class TourInfo
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long Price { get; set; }
        public string Destination { get; set; }
    }
}