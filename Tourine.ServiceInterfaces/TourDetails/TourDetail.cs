using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Places;

namespace Tourine.ServiceInterfaces.TourDetails
{
    public class TourDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Destination))]
        public Guid DestinationId { get; set; }
        [Reference]
        public Destination Destination { get; set; }

        public int Duration { get; set; }
        public DateTime StartDate { get; set; }

        [References(typeof(Place))]
        public Guid PlaceId { get; set; }
        [Reference]
        public Place Place { get; set; }

        public bool IsFlight { get; set; }
        public int InfantPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; }
        public int FoodPrice { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}