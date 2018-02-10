using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/getRootTours","GET")]
    public class GetRootTours : QueryDb<Tour> , IJoin<Tour,TourDetail>
    {
    }
}
