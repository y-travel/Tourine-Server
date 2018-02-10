using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service/{TourId}")]
    public class GetServiceOfTour : QueryDb<PassengerServiceInfo> , IJoin<Passenger,Service>
    {
        public Guid TourId { get; set; }
    }
}