using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/tours/{TourId}/visa/{Have}")]
    public class GetTourVisa : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
        public bool? Have { get; set; } = true;
    }
}
