using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/{TourId}/reserve","POST")]
    public class ReserveBlock : IReturn<Tour>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
        public int Capacity { get; set; }
        public int InfantPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; } 
        public int FoodPrice { get; set; }
        public int BasePrice { get; set; }
    }
}
