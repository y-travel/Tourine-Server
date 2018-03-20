using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{

    [Route("/tours", "GET")]
    public class GetTours : QueryDb<Tour>, IJoin<Tour, TourDetail>
    {
    }

}
