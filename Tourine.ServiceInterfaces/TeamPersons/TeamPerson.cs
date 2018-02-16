using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class TeamPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Team))]
        public Guid TeamId { get; set; }
        [Reference]
        public Team Team { get; set; }

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }
    }
}
