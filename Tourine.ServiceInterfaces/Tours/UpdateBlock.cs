using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/{Id}/reserve/{AgencyId}", "PUT")]
    public class UpdateBlock : IReturn<Tour>
    {
        [QueryDbField(Field = "Id")]
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid AgencyId { get; set; }
        public int Capacity { get; set; }
        public int InfantPrice { get; set; }
        public int BusPrice { get; set; }
        public int RoomPrice { get; set; }
        public int FoodPrice { get; set; }
        public int BasePrice { get; set; }
    }
}
