using ServiceStack;

namespace Tourine.ServiceInterfaces.Places
{
    [Route("/places")]
    public class GetPlaces : QueryDb<Place>
    {
    }
}
