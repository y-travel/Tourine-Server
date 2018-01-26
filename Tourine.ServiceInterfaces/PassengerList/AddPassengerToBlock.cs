using ServiceStack;

namespace Tourine.ServiceInterfaces.PassengerList
{
    [Route("/passengerBlock/", "POST")]
    public class AddPassengerToBlock : IReturn
    {
        public PassengerList PassengerList { get; set; }
    }
}
