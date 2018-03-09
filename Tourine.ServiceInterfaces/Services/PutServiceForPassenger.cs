using ServiceStack;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service", "PUT")]
    public class PutServiceForPassenger : IReturn
    {
        public PassengerList PassengerList { get; set; }
    }
}
