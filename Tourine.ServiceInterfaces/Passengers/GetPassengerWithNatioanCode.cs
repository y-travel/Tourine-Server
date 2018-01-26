using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/{NationalCode}", "GET")]
    public class GetPassengerWithNatioanCode : IReturn<Passenger>
    {
        public string NationalCode { get; set; }
    }
}
