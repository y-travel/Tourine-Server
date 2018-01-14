using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Tour
    {
        [AutoIncrement]
        public Guid Id { get; set; }
        public string Code { get; set; }

        [References(typeof(Destination))]
        public Guid DestinationId { get; set; }
        [Reference]
        public Destination Destination { get; set; }

        [Compute]
        public int Capacity { get; set; }
        public TourStatus Status { get; set; } = TourStatus.Created;
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }

        [References(typeof(Place))]
        public Guid PlaceId { get; set; }
        [Reference]
        public Place Place { get; set; }

        public bool IsFlight { get; set; }
        public int AdultCount { get; set; }
        public int AdultMinPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; }
        public int FoodPrice { get; set; }
        public int InfantPrice { get; set; }
        public int InfantCount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
