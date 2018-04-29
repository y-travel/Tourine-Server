using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class PassengerList
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public OptionType OptionType { get; set; }
        public  bool HaveVisa { get; set; }
        public bool PassportDelivered { get; set; }

        [References(typeof(Team))]
        public Guid TeamId { get; set; }
        [Reference]
        public Team Team { get; set; }
    }
}
