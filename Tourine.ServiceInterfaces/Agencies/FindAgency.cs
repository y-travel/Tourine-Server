using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies/find/str", "GET")]
    public class FindAgency : QueryDb<Agency>
    {
        public string str { get; set; }
    }
}
