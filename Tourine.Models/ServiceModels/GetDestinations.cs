using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.Models.ServiceModels
{
    [Route("/customer/destinations", "GET")]
    public class GetDestinations:QueryDb<Destination>
    {
    }
}
