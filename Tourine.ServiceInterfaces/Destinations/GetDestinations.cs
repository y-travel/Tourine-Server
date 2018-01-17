using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destinations", "GET")]
    public class GetDestinations : QueryDb<Destination>
    {
    }
}
