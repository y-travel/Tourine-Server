using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destinations", "GET")]
    public class GetDestinations : QueryDb<Destination>
    {
    }
}
