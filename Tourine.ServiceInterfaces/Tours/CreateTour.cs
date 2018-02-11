using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/", "POST")]
    public class CreateTour : IReturn<Tour>
    {
        public int Capacity { get; set; }
        public int BasePrice { get; set; }
        public TourDetail TourDetail { get; set; }
    }
}
