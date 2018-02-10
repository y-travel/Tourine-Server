using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/passengerOfTeam/{TeamId}", "GET")]
    public class GetPassengerOfTeam : QueryDb<Passenger> , IJoin<Passenger,TeamPassenger>
    {
        public Guid TeamId { get; set; }
    }
}
