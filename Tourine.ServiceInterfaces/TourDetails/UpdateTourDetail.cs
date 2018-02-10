using ServiceStack;

namespace Tourine.ServiceInterfaces.TourDetails
{
    [Route("/tourDetail", "PUT")]
    public class UpdateTourDetail : IReturn
    {
        public TourDetail TourDetail { get; set; }
    }
}
