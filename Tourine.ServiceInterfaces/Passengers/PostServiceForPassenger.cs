using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/service", "POST")]
    public class PostServiceForPassenger : IReturn<PassengerList>
    {
        public PassengerList PassengerList { get; set; }
    }
}
