using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destination/", "PUT")]
    public class UpdateDestination : IReturn
    {
        public Destination Destination { get; set; }
    }
}
