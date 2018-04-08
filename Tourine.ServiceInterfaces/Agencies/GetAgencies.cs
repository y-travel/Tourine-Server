using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies/{IsAll}","GET")]
    public class GetAgencies : QueryDb<Agency>
    {
        public bool IsAll { get; set; }
    }
}
