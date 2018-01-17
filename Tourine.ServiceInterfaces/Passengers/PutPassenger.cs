using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "PUT")]
    public class PutPassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}