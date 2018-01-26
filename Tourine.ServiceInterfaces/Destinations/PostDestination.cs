using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destination/", "POST")]
    public class PostDestination : IReturn
    {
        public Destination Destination { get; set; }
    }
}
