using ServiceStack;

namespace Tourine.ServiceInterfaces.PassengerList
{
    [Route("/AddPassengerToBlock/", "POST")]
    public class AddPassengerToBlock : IReturn
    {
        public PassengerList PassengerList { get; set; }
    }
}
