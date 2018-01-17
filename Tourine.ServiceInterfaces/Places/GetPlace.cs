using ServiceStack;

namespace Tourine.ServiceInterfaces.Places
{
    [Route("/places")]
    public class GetPlace : QueryDb<Place>
    {
    }
}
