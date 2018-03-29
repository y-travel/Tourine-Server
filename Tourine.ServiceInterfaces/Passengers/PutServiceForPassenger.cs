using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/service", "PUT")]
    public class PutServiceForPassenger : IReturn
    {
        public PassengerList PassengerList { get; set; }
    }
}
