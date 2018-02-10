using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "PUT")]
    public class UpdatePassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}