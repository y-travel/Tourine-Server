using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Places;

namespace Tourine.ServiceInterfaces.Tours
{
    public class Tour
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }

        [References(typeof(Destination))]
        public Guid DestinationId { get; set; }
        [Reference]
        public Destination Destination { get; set; }

        [Compute,Ignore]
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
