using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passengerOf/{AgencyId}/{Str}", "GET")]
    public class FindPassengerInAgency : QueryDb<Passenger>
    {
        public Guid AgencyId { get; set; }
        public string Str { get; set; }
    }
}
