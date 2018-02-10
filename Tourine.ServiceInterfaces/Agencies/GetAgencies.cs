using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies","GET")]
    public class GetAgencies : QueryDb<Agency>
    {
    }
}
