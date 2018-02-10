using ServiceStack;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/passenger/team","POST")]
    public class AddPassengerToTeam : IReturn
    {
        public TeamPassenger TeamPassenger { get; set; }
    }
}
