using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passengers", "GET")]
    public class GetPassengers : QueryDb<Passenger>
    {
    }
}