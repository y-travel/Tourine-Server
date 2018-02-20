using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destinations/{Id}", "GET")]
    [Route("/destinations", "GET")]
    public class GetDestinations : QueryDb<Destination>
    {
        public Guid? Id { get; set; }
    }
}
