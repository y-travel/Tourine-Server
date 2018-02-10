using System;
using ServiceStack;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passengerOf/{AgencyId}/{Str}", "GET")]
    public class FindPassengerInAgency : QueryDb<Passenger> , IJoin<Passenger, TeamPassenger, Team, Tour>
    {
        public Guid AgencyId { get; set; }
        public string Str { get; set; }
    }
}
