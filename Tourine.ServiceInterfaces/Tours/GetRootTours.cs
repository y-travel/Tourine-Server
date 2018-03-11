using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/root","GET")]
    public class GetRootTours : QueryDb<Tour> , IJoin<Tour,TourDetail>
    {
    }
}
