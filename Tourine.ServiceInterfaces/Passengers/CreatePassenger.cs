using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "POST")]
    public class CreatePassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}