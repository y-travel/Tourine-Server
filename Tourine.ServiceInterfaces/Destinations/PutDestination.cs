using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destination/", "PUT")]
    public class PutDestination : IReturn
    {
        public Destination Destination { get; set; }
    }
}
