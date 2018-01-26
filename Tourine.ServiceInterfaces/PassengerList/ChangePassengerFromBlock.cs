using ServiceStack;

namespace Tourine.ServiceInterfaces.PassengerList
{
    [Route("/passengerBlock/", "PUT")]
    public class ChangePassengerFromBlock : IReturn
    {
        public PassengerList PassengerBlock { get; set; }
    }
}
