using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passengers", "GET")]
    public class GetPassengers : QueryDb<Passenger>
    {
    }
}