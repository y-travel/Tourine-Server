using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/service/{TourId}")]
    public class GetServiceOfTour : QueryDb<PassengerList, Person> 
    {
        public Guid TourId { get; set; }
    }
}