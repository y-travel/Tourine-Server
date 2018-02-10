using ServiceStack;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/passenger/team", "PUT")]
    public class ChangePassengerTeam : IReturn
    {
        public TeamPassenger TeamPassenger { get; set; }
    }
}
