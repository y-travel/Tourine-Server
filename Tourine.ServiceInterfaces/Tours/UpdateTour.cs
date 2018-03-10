using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/", "PUT")]
    public class UpdateTour : IReturn<Tour>
    {
        public Tour Tour { get; set; }
    }
}
