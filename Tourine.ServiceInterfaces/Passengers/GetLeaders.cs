using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/leaders","GET")]
    public class GetLeaders : QueryDb<Passenger>
    {
    }
}
