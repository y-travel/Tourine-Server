using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/", "PUT")]
    public class UpdateTour : IReturn
    {
        public Tour Tour { get; set; }
    }
}
