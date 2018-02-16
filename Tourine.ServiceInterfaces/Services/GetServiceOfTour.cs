using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service/{TourId}")]
    public class GetServiceOfTour : QueryDb<Service, Person> 
    {
        public Guid TourId { get; set; }
    }
}