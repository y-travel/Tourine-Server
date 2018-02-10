using ServiceStack;

namespace Tourine.ServiceInterfaces.Destinations
{
    [Route("/destination/", "POST")]
    public class CreateDestination : IReturn<Destination>
    {
        public Destination Destination { get; set; }
    }
}
