using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/{OldTourId}/accomplish","PUT")]
    public class PassengerReplacementTourAccomplish : IReturnVoid
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
        public Guid OldTourId { get; set; }
        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long BusPrice { get; set; }
        public long FoodPrice { get; set; }
        public long RoomPrice { get; set; }
    }
}
