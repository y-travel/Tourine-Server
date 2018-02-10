using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/", "POST")]
    public class PostTour : IReturn<Tour>
    {
        public Tour Tour { get; set; }
    }
}
