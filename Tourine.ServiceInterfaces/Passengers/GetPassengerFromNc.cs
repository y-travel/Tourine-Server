using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/{NationalCode}", "GET")]
    public class GetPassengerFromNc : IReturn<Passenger>
    {
        public string NationalCode { get; set; }
    }
}
