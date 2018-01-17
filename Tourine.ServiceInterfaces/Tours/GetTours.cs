using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{

    [Route("/tours", "GET")]
    public class GetTours : QueryDb<Tour>
    {
    }

}
