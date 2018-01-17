using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/", "POST")]
    public class PostTour : IReturn
    {
        public Tour Tour { get; set; }
    }
}
