using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class TeamPassenger
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Team))]
        public Guid TeamId { get; set; }
        [Reference]
        public Team Team { get; set; }

        [References(typeof(Passenger))]
        public Guid PassengerId { get; set; }
        [Reference]
        public Passenger Passenger { get; set; }
    }
}
