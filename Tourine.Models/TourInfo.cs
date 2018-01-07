using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    [Alias("Tour")]
    public class TourInfo
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        [References(typeof(PriceDetail))]
        public Guid PriceDetailId { get; set; }
        [Reference]
        public PriceDetail PriceDetail { get; set; }
        [Reference]
        public Destination Destination { get; set; }
        [Reference]
        public Place Place { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public bool IsFlight { get; set; }
        public int AdultCount { get; set; }
        [Ignore]
        public int InfantCount { get; set; }
        public int AdultMinPrice { get; set; }
        public int InfantPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; }
        public int FoodPrice { get; set; }
    }
}