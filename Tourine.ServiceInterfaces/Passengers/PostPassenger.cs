using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "POST")]
    public class PostPassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}