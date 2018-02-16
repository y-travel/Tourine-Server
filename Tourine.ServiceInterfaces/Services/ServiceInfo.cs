using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Services
{
    public class ServiceInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }

        [References(typeof(Tour))]
        public int TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public ServiceType Type { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
