using ServiceStack;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service", "POST")]
    public class PostServiceForPassenger : IReturn<PassengerList>
    {
        public PassengerList PassengerList { get; set; }
    }
}
